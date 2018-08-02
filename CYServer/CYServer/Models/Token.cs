using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYServer.Models
{
    internal class Token
    {
        [JsonProperty] 
        public string access_token;
        [JsonProperty] 
        public int expires_in;
        [JsonProperty]
        public string refresh_token;
        [JsonProperty]
        public string scope;
        [JsonProperty]
        public string session_key;
        [JsonProperty]
        public string session_secret;

        /*
        internal bool TokenValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(access_token)
                    || string.IsNullOrWhiteSpace(refresh_token)) 
                    return false;
                var expire = DataTimeOffset.FromUnixTimeSeconds()
            }
        }
        */

    }
}