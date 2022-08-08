//using Domain.Models;
//using Moq;
//using Services.Interface;
//using Services.Query;
//using Services.Services;
//using UnitTest.Base;
//using Xunit;

//namespace UnitTest.WeatherMeasurement
//{
//    public class GetDataInfoSensorsTest 
//    {
//        [Theory]
//        //[InlineData("2018-12-25 23:50:55.999", 0,01)]
//        //[InlineData("2018-12-25 23:50:55.999", 9,99)]
//        //[InlineData("2018-12-25 23:50:55.99", 99,99)]
//        [InlineData("sensorX", "deviceY")]
//        [InlineData("sensorX", "deviceY", "2018-12-25 23:50:55")]
//        public void CheckServiceIsWorking(string sensor, string device, DateTime date)
//        {
//            var qry = new CollectMeasurementsQuery("sensorX", "deviceY");
//            var listresponse = Task.FromResult(new List<WeatherModel>
//            {
//                new WeatherModel(DateTimeOffset.Now, 2),
//                new WeatherModel(DateTimeOffset.Now, 01),
//                //new WeatherModel(DateTimeOffset.Now, decimal.MinValue),
//                //new WeatherModel(DateTimeOffset.UtcNow, decimal.MaxValue)
//            });

//            // Arrange
//            Mock<IDataInfoSensorsServices> mock = new Mock<IDataInfoSensorsServices>();
//            mock.Setup(x => x.GetDataSensors(qry)).Returns(listresponse);
//            DataInfoSensorsServices dataInfoSensorsServices = new DataInfoSensorsServices();

//            // Act
//            var resultWaited = mock.Object.GetDataSensors(qry);
//            var result = dataInfoSensorsServices.GetDataSensors(qry).Result;

//            // Assert
//            Assert.Equal(2, result.Count);
//        }

//        //public void CheckReturn(string sensor, string device, DateTime date)
//        //{
//        //}
//    }
//}
