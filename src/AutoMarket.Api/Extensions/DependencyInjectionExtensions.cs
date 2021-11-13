using AutoMapper;
using AutoMarket.Api.Constants;
using AutoMarket.Api.Helpers;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Infrastructures.Cache.Redis;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Infrastructures.Middlewares;
using AutoMarket.Api.Models;
using AutoMarket.Api.Repostories;
using AutoMarket.Api.Repostories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var settings = new AppSettingsModel();

            configuration.Bind("AppSettings", settings);

            services.AddScoped(sp =>
            {
                var httpContext = (sp.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext;
                if (httpContext == null || !httpContext.Request.Headers.ContainsKey("Authorization"))
                    return null;

                string token = httpContext.Request.Headers.FirstOrDefault(e => e.Key == "Authorization").Value.ToString();
                var model = SecurityHelper.ValidateToken(token, settings.Secret);

                return model;
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = CommonConstants.SERVICE_NAME, Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    });
            });
            return services;
        }

        public static IApplicationBuilder UseMiddlewares (this IApplicationBuilder app)
        {
            if(app == null)
                throw new ArgumentNullException(nameof(app)); ;

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<TokenMiddleware>();
            app.UseMiddleware<RequestPerformanceMiddleware>();

            return app;
        }
    }
}
