using AutoMarket.Api.Constants;
using AutoMarket.Api.Extensions;
using AutoMarket.Api.Helpers;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Middlewares;
using AutoMarket.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace AutoMarket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettingsModel>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<AutoMarketDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "AutoMarketDb"));
            services.AddRedisManager(Configuration)
                .AddAutoMapper()
                .AddUserModel(Configuration)
                .AddRepositories()
                .AddMediatR(Assembly.Load(CommonConstants.SERVICE_NAME))
                .AddApiVersion()
                .AddControllers();

            Log.Logger = LoggingHelper.CustomLoggerConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = CommonConstants.SERVICE_NAME, Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{CommonConstants.SERVICE_NAME} v1"));
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<TokenMiddleware>();
            app.UseMiddleware<RequestPerformanceMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
