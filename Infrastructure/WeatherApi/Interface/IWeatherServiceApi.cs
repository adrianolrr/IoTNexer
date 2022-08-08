using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WeatherApi.Interface
{
    public interface IWeatherServiceApi
    {
        Task<string> ReadFileAsync(string filename, string containerName);
    }
}
