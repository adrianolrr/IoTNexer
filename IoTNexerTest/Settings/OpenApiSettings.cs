using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNexerTest.Settings
{
    public class OpenApiSettings : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "Nexer Test",
            Description = "API de weather data collect with .NET 6 + Azure Functions",
            Contact = new OpenApiContact()
            {
                Name = "Adriano Ramos",
                Url = new Uri("https://github.com/adrianolrr"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        };

        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    }
}
