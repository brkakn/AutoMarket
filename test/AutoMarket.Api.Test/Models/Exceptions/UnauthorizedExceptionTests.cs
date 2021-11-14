using AutoMarket.Api.Constants;
using AutoMarket.Api.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Xunit;

namespace AutoMarket.Api.Test.Models.Exceptions
{
    public class UnauthorizedExceptionTests
    {
        private readonly int _expectedStatusCode = StatusCodes.Status401Unauthorized;
        private readonly string _expectedTitle = ExceptionMessageKeyConstants.UNAUTHORIZED_TITLE;
        private readonly string _expectedDetail = ExceptionMessageKeyConstants.UNAUTHORIZED_DETAIL;

        [Fact]
        public void Constructor_ShouldAssertTrue_Default()
        {
            //Act
            var result = new UnauthorizedException();

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(_expectedTitle, result.ProblemDetailsModel.Title);
            Assert.Equal(_expectedDetail, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test message")]
        public void Constructor_ShouldAssertTrue_Message(string message)
        {
            //Act
            var result = new UnauthorizedException(message);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(message, result.ProblemDetailsModel.Detail);
            Assert.Equal(message, result.ProblemDetailsModel.Title);
        }

        [Theory, InlineData("test message")]
        public void Constructor_ShouldAssertTrue_MessageList(string message)
        {
            //Arrange
            var messageList = new List<string>() { message };

            //Act
            var result = new UnauthorizedException(messageList);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test title", "test message")]
        public void Constructor_ShouldAssertTrue_TitleAndMessageList(string title, string message)
        {
            // Arrange
            var messageList = new List<string>() { message };

            //Act
            var result = new UnauthorizedException(messageList, title);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(title, result.ProblemDetailsModel.Title);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test title", "test message")]
        public void Constructor_ShouldAssertTrue_TitleAndMessage(string title, string message)
        {
            //Act
            var result = new UnauthorizedException(message, title);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(title, result.ProblemDetailsModel.Title);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }
    }
}
