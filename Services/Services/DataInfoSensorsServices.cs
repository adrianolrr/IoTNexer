using Domain.Models;
using Infrastructure.WeatherApi.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Interface;
using Services.Models;
using Services.Settings;

namespace Services.Services
{
    public class DataInfoSensorsServices : IDataInfoSensorsServices
    {
        private readonly IWeatherServiceApi _wheatherServiceApi;
        private readonly IOptions<WeatherSettings> _wheatherSettings;
        private readonly ILogger _logger;

        public DataInfoSensorsServices(IWeatherServiceApi weatherServiceApi,
            IOptions<WeatherSettings> wheatherSettings,
            ILogger<DataInfoSensorsServices> logger)
        {
            _wheatherServiceApi = weatherServiceApi;
            _wheatherSettings = wheatherSettings;
            _logger = logger;
        }
        public async Task<List<WeatherModel>> GetDataSensors(MeasurementsModel qry)
        {
            _logger.LogInformation($"Init loading gedatasensors... ${qry}");
            try
            {
                string contentFile = string.Empty;
                string filename = qry.Date == null ? "historical.zip" : $"{qry.Date.Value.ToString("yyyy-MM-dd")}.csv";
                contentFile = await _wheatherServiceApi.ReadFileAsync(filename, $"{qry.Device}/{qry.SensorType}");
                var listWeather = new List<WeatherModel>();
                if(string.IsNullOrEmpty(contentFile))
                    return new List<WeatherModel>();
                foreach (var line in contentFile.Split("\r\n"))
                {
                    string value = line.Split(";")[1].Substring(0, 1).Equals(",") ? "0" + line.Split(";")[1] : line.Split(";")[1];
                    listWeather.Add(new WeatherModel(line.Split(";")[0].Trim(), Convert.ToDecimal(value)));
                }
                return listWeather;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                _logger.LogInformation($"Ended loading gedatasensors... ${qry}");
            }
        }
    }
}
