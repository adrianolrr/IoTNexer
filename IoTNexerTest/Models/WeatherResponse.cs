using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using static IoTNexerTest.WeatherFunctions.CollectMeasurementsByDayAndSensorAndUnitHttpTrigger;

namespace IoTNexerTest.Models
{
    [OpenApiExample(typeof(WeatherResponseExample))]
    public class WeatherResponse
    {
        public WeatherResponse()
        {

        }
        public WeatherResponse(string date, decimal value)
        {
            Date = date;
            Value = value;
        }

        [OpenApiProperty]
        public string Date { get; set; }
        [OpenApiProperty]
        public decimal Value { get; set; }
    }
}
