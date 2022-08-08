using Infrastructure.Settings;
using Infrastructure.WeatherApi;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
    public abstract class AzureIoTBase
    {
        public readonly ILogger _logger;
        public readonly AzureIoTSettings _azureIoTSettings;
        public AzureIoTBase(ILogger<WeatherServiceApi> logger,IOptions<AzureIoTSettings> azureIoTSettings)
        {
            _azureIoTSettings = azureIoTSettings.Value;
            _logger = logger;
        }
    }
}
