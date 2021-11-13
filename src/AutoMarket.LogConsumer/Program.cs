using AutoMarket.LogConsumer.Infrastructure.ElasticContext;
using AutoMarket.LogConsumer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AutoMarket.LogConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", reloadOnChange: false, optional: false)
                .Build();

            IServiceCollection services = new ServiceCollection()
                                        .Configure<RabbitMqConfigModel>(Configuration.GetSection("RabbitMqConfig"))
                                        .Configure<ElasticSearchConfigModel>(Configuration.GetSection("ElasticSearchConfig"))
                                        .AddSingleton<IElasticContext, ElasticContext>()
                                        .AddSingleton<Consumer>();

            // entry to run app
            await services.BuildServiceProvider().GetService<Consumer>().Run(args);
        }
    }
}
