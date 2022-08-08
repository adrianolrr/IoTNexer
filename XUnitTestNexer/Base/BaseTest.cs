using Microsoft.Extensions.Options;
using Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestNexer.Base
{
    public class BaseTest
    {
        public string uri;
        public string connection;
        public string stringRead;
        public IOptions<WeatherSettings> weatherSettings;
        public BaseTest()
        {
            uri = "nexer-blob-test";
            connection = "DefaultEndpointsProtocol=https;AccountName=storaaccountnamev1;AccountKey=HDHu94h/YvMlxdJmqHWLDnpYXa0IBsQAnvCmW7kcrlE7ET3t7VkSuabGyP7/JF1nqYRIBNX6s2/y+AStVqpvTw==;EndpointSuffix=core.windows.net";
            stringRead = @"2022-08-04T12:20:22;,04\r\n2022-08-04T17:20:23;,04\r\n2022-08-04T11:10:24;,04\r\n2022-08-04T12:05:22;,04\r\n2022-08-03T10:20:22;,04\r\n2022-08-03T09:20:22;,50\r\n2022-08-03T04:20:22;,40";
            weatherSettings = Options.Create(new WeatherSettings() { Uri = "nexer", Connection = "DefaultEndpointsProtocol=https;AccountName=storaaccountnamev1;AccountKey=HDHu94h/YvMlxdJmqHWLDnpYXa0IBsQAnvCmW7kcrlE7ET3t7VkSuabGyP7/JF1nqYRIBNX6s2/y+AStVqpvTw==;EndpointSuffix=core.windows.net" });
        }
    }
}

