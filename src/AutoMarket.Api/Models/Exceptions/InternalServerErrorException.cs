using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.INTERNAL_SERVER_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.INTERNAL_SERVER_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }
    }
}
