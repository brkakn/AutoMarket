using AutoMarket.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoMarket.Api.Models.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstants.NOTFOUND_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstants.NOTFOUND_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(List<string> messages, string title) : base(messages, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }
    }
}
