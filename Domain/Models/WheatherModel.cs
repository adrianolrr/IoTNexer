using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WeatherModel
    {
        public WeatherModel(string date, decimal value)
        {
            Date = date;
            Value = value;
        }

        public string Date { get; set; }
        public decimal Value { get; set; }
    }
}
