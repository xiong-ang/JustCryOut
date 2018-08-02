using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CYServer.Models
{
    class AjaxHelper
    {
        public static bool GetCall(string UriLink, string action, out string answer)
        {
            answer = string.Empty;
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(UriLink + action);
                request.Method = HttpMethod.Get;
                //Add some special info
                //request.Headers.Add..
                return SendCall(request, out answer);
            }
        }
        public static bool PostCall(string UriLink, string content, string action, out string answer)
        {
            answer = string.Empty;
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(UriLink + action);
                request.Method = HttpMethod.Post;
                //Add some special info
                //request.Headers.Add..
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                return SendCall(request, out answer);
            }
        }
        private static bool SendCall(HttpRequestMessage req, out string answer)
        {
            //To Add Common Header Info, such as : Token, Client_id, Client_secret
            //req.Headers.Add...  

            answer = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var res = client.SendAsync(req, HttpCompletionOption.ResponseContentRead).Result;
                    answer = res.Content.ReadAsStringAsync().Result;
                    return res.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}