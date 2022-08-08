using System.Collections.Generic;
using System.Net;
using IoTNexerTest.Models;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Services.Query;

namespace IoTNexerTest.WeatherFunctions
{
    public class CollectMeasurementsByDayAndUnitHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public CollectMeasurementsByDayAndUnitHttpTrigger(ILoggerFactory loggerFactory, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<CollectMeasurementsByDayAndUnitHttpTrigger>();
            _mediator = mediator;
        }

        // <remarks>
        // Sample request:
        // 
        //     GET v1/devices/{deviceId}/data/{sensorType}
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
        [Function("getdatafordevice")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "getdatafordevice" })]
        [OpenApiParameter(name: "deviceId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The device parameter is required")]
        [OpenApiParameter(name: "sensorType", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The sensortype parameter is required")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Return list dates and values measurements.")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", 
            Route = "v1/devices/{deviceId}/data/{sensorType}")] HttpRequestData req, 
            string deviceId, 
            string sensorType)
        {
            _logger.LogInformation($"Init loading {nameof(CollectMeasurementsByDayAndSensorAndUnitHttpTrigger)}... {req}");
            try
            {
                if (string.IsNullOrEmpty(deviceId))
                    new ArgumentException("Device is required!");
                if (string.IsNullOrEmpty(sensorType))
                    new ArgumentException("Sensor is required!");

                var qry = new CollectMeasurementsQuery(deviceId, sensorType);
                var result = await _mediator.Send(qry);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message);
                return response;
            }
            finally
            {
                _logger.LogInformation($"Ended loading {nameof(CollectMeasurementsByDayAndSensorAndUnitHttpTrigger)}.. ");
            }
        }
    }
}
