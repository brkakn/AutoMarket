using AutoMapper;
using AutoMarket.Api.Constants;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Infrastructures.Cache.Redis;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRedisManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfigModel>(configuration.GetSection(CommonConstants.CACHE_CONFIG_KEY));
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
