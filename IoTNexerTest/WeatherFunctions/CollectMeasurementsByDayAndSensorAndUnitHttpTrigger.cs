using System.Collections.Generic;
using System.Net;
using Domain.Models;
using IoTNexerTest.Extensions;
using IoTNexerTest.Models;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Services.Query;

namespace IoTNexerTest.WeatherFunctions
{
    public class CollectMeasurementsByDayAndSensorAndUnitHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public CollectMeasurementsByDayAndSensorAndUnitHttpTrigger(
            ILoggerFactory loggerFactory,
            IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<CollectMeasurementsByDayAndSensorAndUnitHttpTrigger>();
            _mediator = mediator;
        }

        // <remarks>
        // Sample request:
        // 
        //     GET v1/devices/{deviceId}/data/{date}/{sensorType}
        //     [
        //        {
        //            "date": "2022-08-04T12:20:22",
        //            "value": 0.04
        //        },
        //        {
        //            "date": "2022-08-04T17:20:23",
        //            "value": 0.04
        //        },
        //       {
        //       "date": "2022-08-04T11:10:24",
        //            "value": 0.04
        //        },
        //    ]
        // </remarks>
        [Function("getdata")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "getdata" })]
        [OpenApiParameter(name: "deviceId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The device parameter is required")]
        [OpenApiParameter(name: "date", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The date parameter is required")]
        [OpenApiParameter(name: "sensorType", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The sensortype parameter is required")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(string), Description = "Return list dates and values measurements.")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET",
            Route = "v1/devices/{deviceId}/data/{date}/{sensorType}")] HttpRequestData req,
            string deviceId,
            string sensorType,
            string date)
        {
            _logger.LogInformation($"Init loading {nameof(CollectMeasurementsByDayAndSensorAndUnitHttpTrigger)}... {req}");
            try
            {
                if (string.IsNullOrEmpty(deviceId))
                    new ArgumentException("Device is required!");
                if (string.IsNullOrEmpty(sensorType))
                    new ArgumentException("Sensor is required!");
                DateTime dateSensor;
                if (DateTime.TryParse(date, out dateSensor))
                    new ArgumentException("DateTime format is required!");

                var qry = new CollectMeasurementsQuery(deviceId, sensorType, dateSensor);

                var result = await _mediator.Send(qry);
                var response = req.CreateResponse(result.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent);
                _ = response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteStringAsync(ex.Message);
                return response;
            }
            finally
            {
                _logger.LogInformation($"Ended loading {nameof(CollectMeasurementsByDayAndSensorAndUnitHttpTrigger)}.. ");
            }
        }

        public class WeatherResponseExample : OpenApiExample<WeatherResponse>
        {
            public override IOpenApiExample<WeatherResponse> Build(NamingStrategy namingStrategy = null)
            {
                var listresponse = new List<WeatherResponse>();
                listresponse.Add(new WeatherResponse("2022-08-04T12:20:22", 04));
                listresponse.Add(new WeatherResponse("2022-08-04T12:20:22", 05));
                this.Examples.Add(
                    OpenApiExampleResolver.Resolve(
                        "Samples Response",
                        listresponse,
                        namingStrategy
                    ));

                return this;
            }
        }
    }
}
