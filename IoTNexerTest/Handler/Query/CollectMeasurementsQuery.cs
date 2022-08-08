using IoTNexerTest.Models;
using MediatR;

namespace Services.Query
{
    public class CollectMeasurementsQuery : IRequest<List<WeatherResponse>>//, IValidateable<CollectMeasurementsQuery>
    {
        public CollectMeasurementsQuery()
        {

        }
        public CollectMeasurementsQuery(string device, string sensorType, DateTime? date = null)
        {
            Date = date;
            Device = device;
            SensorType = sensorType;
        }

        public DateTime? Date { get; set; }
        public string Device { get; set; }
        public string SensorType { get; set; }

        //[JsonIgnore]
        //public AbstractValidator<CollectMeasurementsQuery> Validator { get => CollectMeasurementsQueryValidator.Instance; }

    }
}
