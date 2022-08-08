using Domain.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IDataInfoSensorsServices
    {
        Task<List<WeatherModel>> GetDataSensors(MeasurementsModel qry);
    }
}
