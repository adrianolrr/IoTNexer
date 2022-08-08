using Domain.Models;
namespace Services.Models
{
    public class MeasurementsModel
    {
        public MeasurementsModel()
        {

        }
        public MeasurementsModel(string device, string sensorType, DateTime? date = null)
        {
            Date = date;
            Device = device;
            SensorType = sensorType;
        }

        public DateTime? Date { get; set; }
        public string Device { get; set; }
        public string SensorType { get; set; }

    }
}