using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Testing
{
    public static class Jsonning
    {
        public static string SerializeJsonObjectWithCamelCasePropertyNames(object target)
        {
            return JsonConvert.SerializeObject(target, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public static JObject ConvertToCamelCasePropertyJObject(object target)
        {
            return JObject.Parse(JsonConvert.SerializeObject(target, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            }));
        }

        public static JObject ConvertToJObject(object target)
        {
            return JObject.Parse(JsonConvert.SerializeObject(target, Formatting.None, new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            }));
        }
    }
}
