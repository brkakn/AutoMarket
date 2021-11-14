using AutoMarket.Api.Models;
using AutoMarket.Api.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Api.Infrastructures.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _contentTypeList;

        public ExceptionHandlingMiddleware(
            RequestDelegate next
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _contentTypeList = new();
            _contentTypeList.Add("application/json");
            _contentTypeList.Add("application/x-www-form-urlencoded");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context?.Request?.ContentType) && !_contentTypeList.Contains(context.Request.ContentType))
                    throw new NotAcceptableException();
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/problem+json";
                var baseException = exception is BaseException ? (BaseException)exception : new InternalServerErrorException();
                baseException.ProblemDetailsModel.Instance = context.Request.Path;
                baseException.ProblemDetailsModel.Type = context.Request.Path;
                context.Response.StatusCode = baseException.ProblemDetailsModel.Status.Value;

                DateTime end = DateTime.Now;
                context.Request.EnableBuffering();

                var userModel = context.RequestServices.GetService(typeof(UserModel)) as UserModel;

                var body = context.Request.Body;
                var payload = "";
                if (body.CanSeek)
                {
                    body.Seek(0, SeekOrigin.Begin);
                    payload = new StreamReader(body).ReadToEnd();
                }

                var log = new
                {
                    Issuer = userModel?.UserName,
                    QueryString = context.Request.QueryString.Value,
                    Path = context.Request.Path,
                    RequestorIpAddress = context.Connection.RemoteIpAddress.ToString(),
                    Url = context.Request.GetDisplayUrl(),
                    RequestData = GetRequestData(context)
                };
            }
        }

        private static string GetRequestData(HttpContext context)
        {
            var sb = new StringBuilder();

            if (context.Request.HasFormContentType && context.Request.Form.Any())
            {
                sb.Append("Form variables:");
                foreach (var x in context.Request.Form)
                    sb.AppendFormat("Key={0}, Value={1}<br/>", x.Key, x.Value);
            }
            sb.AppendLine("Method: " + context.Request.Method);
            return sb.ToString();
        }
    }
}
