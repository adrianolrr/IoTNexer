using Infrastructure.Settings;
using Infrastructure.WeatherApi;
using Infrastructure.WeatherApi.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;
using Services.Services;
using Services.Settings;
using System.Reflection;

namespace IoTNexerTest.Extensions
{
    public static class IoCExtensions
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<WeatherSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("WeatherSettings").Bind(settings);
                });
            services.AddOptions<AzureIoTSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("AzureIoTSettings").Bind(settings);
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IDataInfoSensorsServices, DataInfoSensorsServices>();
            services.AddTransient<IWeatherServiceApi, WeatherServiceApi>();
        }
    }
}
