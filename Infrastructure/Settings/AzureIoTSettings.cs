using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
{
    public class AzureIoTSettings
    {
        public string Uri { get; set; }
        public string Name { get; set; }
        public string Blob { get; set; }
        public string Queue { get; set; }
        public string File { get; set; }
        public string Table { get; set; }
        public string Connection { get; set; }
        public string SasToken { get; set; }
        public string Key { get; set; }
    }
}
