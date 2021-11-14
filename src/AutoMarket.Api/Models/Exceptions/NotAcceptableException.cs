using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class NotAcceptableException : BaseException
    {
        public NotAcceptableException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.NOTACCEPTABLE_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.NOTACCEPTABLE_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }
    }
}
