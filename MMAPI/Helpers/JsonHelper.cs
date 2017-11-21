using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

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
