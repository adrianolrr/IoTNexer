using Infrastructure.WeatherApi.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Services.Interface;
using Services.Models;
using Services.Services;
using Services.Settings;
using XUnitTestNexer.Base;

namespace XUnitTestNexer
{
    public class GetDataInfoSensorsUnitTest : BaseTest
    {
        [Theory]
        [InlineData("sensorX", "deviceY")]
        public void NotFoundDataWithoutDateTest(string sensor, string device)
        {
            var mockIlogger = new Mock<ILogger<DataInfoSensorsServices>>();
            _ = mockIlogger.Object;
            ILogger<DataInfoSensorsServices> logger = Mock.Of<ILogger<DataInfoSensorsServices>>();

            var mockIWeatherApi = new Mock<IWeatherServiceApi>();
            _ = mockIWeatherApi.Object;
            IWeatherServiceApi weatherApi = Mock.Of<IWeatherServiceApi>();

            var weatherSettings = Options.Create(new WeatherSettings(){Connection = connection, Uri = uri });

            var qry = new MeasurementsModel(sensor, device);
            
            // Arrange
            Mock<IDataInfoSensorsServices> mock = new();
            DataInfoSensorsServices dataInfoSensorsServices = new(weatherApi, weatherSettings, logger);

            // Act
            _ = mock.Object.GetDataSensors(qry);
            var result = dataInfoSensorsServices.GetDataSensors(qry).Result;

            // Assert
            Assert.True(!result.Any());
        }

        [Theory]
        [InlineData("sensorX", "deviceY", "2022-08-04T12:20:22")]
        public void NotFoundDataWithDateTest(string sensor, string device, DateTime date)
        {
            var mockIlogger = new Mock<ILogger<DataInfoSensorsServices>>();
            _ = mockIlogger.Object;
            ILogger<DataInfoSensorsServices> logger = Mock.Of<ILogger<DataInfoSensorsServices>>();

            var mockIWeatherApi = new Mock<IWeatherServiceApi>();
            _ = mockIWeatherApi.Object;
            IWeatherServiceApi weatherApi = Mock.Of<IWeatherServiceApi>();

            var weatherSettings = Options.Create(new WeatherSettings(){ Connection = connection, Uri = uri });

            var qry = new MeasurementsModel(sensor, device, date);

            // Arrange
            Mock<IDataInfoSensorsServices> mock = new();
            DataInfoSensorsServices dataInfoSensorsServices = new(weatherApi, weatherSettings, logger);

            // Act
            _ = mock.Object.GetDataSensors(qry);
            var result = dataInfoSensorsServices.GetDataSensors(qry).Result;

            // Assert
            Assert.True(!result.Any());
        }
    }
}