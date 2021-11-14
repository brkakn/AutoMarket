using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.BAD_REQUEST_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.BAD_REQUEST_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }
        public BadRequestException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }
    }
}
