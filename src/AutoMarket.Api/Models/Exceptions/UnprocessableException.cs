using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class UnprocessableException : BaseException
    {
        public UnprocessableException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.UNPROCCESABLE_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.UNPROCCESABLE_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
