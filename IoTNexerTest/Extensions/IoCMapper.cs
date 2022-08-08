using Domain.Models;
using IoTNexerTest.Models;
using Microsoft.Extensions.DependencyInjection;
using Services.Models;
using Services.Query;

namespace IoTNexerTest.Extensions
{
    public static class IoCMapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<WeatherModel, WeatherResponse>().ReverseMap();
                config.CreateMap<CollectMeasurementsQuery, MeasurementsModel>().ReverseMap();
            });
        }
    }
}
