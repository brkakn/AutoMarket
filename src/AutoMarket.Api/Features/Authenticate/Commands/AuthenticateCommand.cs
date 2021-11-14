using AutoMarket.Api.Constants;
using AutoMarket.Api.Features.Authenticate.Models;
using AutoMarket.Api.Helpers;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Models;
using AutoMarket.Api.Models.Exceptions;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.Authenticate.Commands
{
    public class AuthenticateCommand : IRequest<TokenModel>
    {
        public AuthenticateCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, TokenModel>
    {
        private readonly AppSettingsModel _appSettingsModel;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        public AuthenticateCommandHandler(
            IOptions<AppSettingsModel> appSettings,
            IUserRepository userRepository,
            ICacheService cacheService)
        {
            _appSettingsModel = appSettings.Value;
            _userRepository = userRepository;
            _cacheService = cacheService;
        }

        public async Task<TokenModel> Handle(AuthenticateCommand request, CancellationToken ct)
        {
            var user = await _userRepository.AuthenticateUser(request.UserName, request.Password, ct);
            if (user == null)
                throw new BadRequestException("UserName or Password wrong!");

            var token = SecurityHelper.GenerateToken(user, _appSettingsModel.Secret);

            await _cacheService.Add($"{CacheConstants.USER_INFO}{user.Id}", token, TimeSpan.FromHours(6));

            return new TokenModel(token);
        }
    }
}
