using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.UNAUTHORIZED_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.UNAUTHORIZED_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }
    }
}
