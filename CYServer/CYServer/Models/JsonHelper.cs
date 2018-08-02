using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYServer.Models
{
    class JsonHelper
    {
        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string ToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj, Setting);
        }

        public static T FormJson<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(json, Setting);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}