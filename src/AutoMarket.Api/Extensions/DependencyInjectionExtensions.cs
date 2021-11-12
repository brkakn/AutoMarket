using AutoMapper;
using AutoMarket.Api.Constants;
using AutoMarket.Api.Helpers;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Infrastructures.Cache.Redis;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Models;
using AutoMarket.Api.Repostories;
using AutoMarket.Api.Repostories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AutoMarket.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRedisManager(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<CacheConfigModel>(configuration.GetSection(CommonConstants.CACHE_CONFIG_KEY));
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IShoppingCartDetailRepository, ShoppingCartDetailRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            return services;
        }

        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = ApiVersion.Default;
            });

            return services;
        }

        public static IServiceCollection AddUserModel(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var settings = new AppSettingsModel();

            configuration.Bind("AppSettings", settings);

            services.AddScoped(sp =>
            {
                var httpContext = (sp.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext;
                if (httpContext == null || !httpContext.Request.Headers.ContainsKey("token"))
                    return null;

                string token = httpContext.Request.Headers.FirstOrDefault(e => e.Key == "token").Value.ToString();
                var model = SecurityHelper.ValidateToken(token, settings.Secret);

                return model;
            });

            return services;
        }
    }
}
