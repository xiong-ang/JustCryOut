using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYServer.Models
{
    public class Token
    {
        [JsonProperty] 
        public string access_token;
        [JsonProperty] 
        public long expires_in;
        [JsonProperty]
        public string refresh_token;
        [JsonProperty]
        public string scope;
        [JsonProperty]
        public string session_key;
        [JsonProperty]
        public string session_secret;

        [JsonIgnore]
        private DateTime tokenAcquiredTime;

        [JsonIgnore]
        public bool TokenValid
        {
            get
            {
                return (!string.IsNullOrWhiteSpace(access_token)
                    && !string.IsNullOrWhiteSpace(refresh_token)
                    && DateTime.Now < tokenAcquiredTime.AddSeconds(expires_in - 5 * 60));
            }
        }

        [JsonIgnore]
        public bool RefreshTokenValid
        {
            get
            {
                //Need to get the info
                return false;
            }
        }

        public static Token NewToken(string json, Token oldToken = null)
        {
            Token token = null;
            try
            {
                token = JsonHelper.FormJson<Token>(json);
                token.tokenAcquiredTime = DateTime.Now;

                //If get token from refresh token
                if (string.IsNullOrEmpty(token.refresh_token) && oldToken != null && !string.IsNullOrWhiteSpace(oldToken.refresh_token))
                {
                    token.refresh_token = oldToken.refresh_token;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return token;
        }
    }
}