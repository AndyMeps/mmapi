using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Helpers
{
    public static class JsonHelper
    {
        public static JsonMediaTypeFormatter StandardFormatter
        {
            get
            {
                return new JsonMediaTypeFormatter
                {
                    SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    }
                };
            }
        }
    }
}
