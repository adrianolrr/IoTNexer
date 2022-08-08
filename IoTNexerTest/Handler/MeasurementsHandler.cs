using AutoMapper;
using IoTNexerTest.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Interface;
using Services.Models;
using Services.Query;

namespace IoTNexerTest.Handler
{
    public class MeasurementsHandler : IRequestHandler<CollectMeasurementsQuery, List<WeatherResponse>>
    {
        private readonly IDataInfoSensorsServices _dataInfoSensorsServices;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public MeasurementsHandler(IDataInfoSensorsServices dataInfoSensorsServices,
            IMapper mapper,
            ILogger<MeasurementsHandler> logger)
        {
            _dataInfoSensorsServices = dataInfoSensorsServices;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<WeatherResponse>> Handle(CollectMeasurementsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Init loading handle... ${request}");
            try
            {
                var dtoQry = _mapper.Map<MeasurementsModel>(request);
                var dto = await _dataInfoSensorsServices.GetDataSensors(dtoQry);
                return _mapper.Map<List<WeatherResponse>>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                _logger.LogInformation("Ended loading handle...");
            }
        }
    }
}
