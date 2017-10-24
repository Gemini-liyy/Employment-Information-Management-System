using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Tinge.Extension
{
    public static class JsonExtension
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="camelCase"></param>
        /// <param name="indented"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = true, bool indented = true)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            options.Converters.Insert(0, new IsoDateTimeConverter());
            //options.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //options.PreserveReferencesHandling = PreserveReferencesHandling.Arrays;

            return JsonConvert.SerializeObject(obj, options);
        }
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format">"yyyy-MM-dd HH:mm"</param>
        /// <returns></returns>
        public static string ToJsonFormat(this object obj, string format)
        {
            if (!string.IsNullOrEmpty(format))
                return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter() { DateTimeFormat = format });
            else
                return ToJsonString(obj);
        }
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return ToJsonString(obj, false, false);
        }
    }
}
