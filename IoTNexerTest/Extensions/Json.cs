using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNexerTest.Extensions
{
    public static class Json
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
        public static T FromJson<T>(this object obj) => JsonConvert.DeserializeObject<T>(obj as string);
    }
}
