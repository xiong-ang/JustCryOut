using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CYServer.Models
{
    public class AccessManager
    {
        //Access Info
        private static readonly string GrantType = "client_credentials";
        private static readonly string ClientId = "PIX9PCkB41qRmmopUcFi2CrY";
        private static readonly string ClientSecret = "rDSRUf2GCHh5H9oMHdmH1cYgNV4WYPYL";

        private static readonly string ServerUrl = @"https://openapi.baidu.com/oauth/2.0/token";

        #region signalton
        private static AccessManager _instance;
        private static object SLock = new object();
        public static AccessManager Instance
        {
            get 
            {
                lock (SLock)
                {
                    return _instance??(_instance = new AccessManager());
                }
            }
        }
        private AccessManager()
        {
        }
        #endregion signalton

        private Token token;
        public Token AMToken
        {
            get 
            {
                if (null == token)
                    token = GetNewToken();

                //Refresh token
                if (!token.TokenValid && token.RefreshTokenValid)
                    token = RefreshToken(token);
                return token.TokenValid ? token : null; 
            }
        }

        public void Login()
        {
            token = GetNewToken();
        }

        public void Logout()
        {
            token = null;
        }

        private Token GetNewToken()
        {
            Token tk = null;
            try
            {
                var uri = new UriBuilder(AccessManager.ServerUrl);
                uri.Query = new UriParamsComposer
            {
                {"grant_type",AccessManager.GrantType},
                {"client_id",AccessManager.ClientId},
                {"client_secret",AccessManager.ClientSecret}
            }
                .ToString();
                string answer;
                AjaxHelper.GetCall(uri.ToString(), string.Empty, out answer);
                tk = Token.NewToken(answer);
            }
            catch (Exception)
            {
                throw;
            }

            return tk;
        }
        private Token RefreshToken(Token token)
        {
            throw new NotImplementedException();
        }
        
    }
}