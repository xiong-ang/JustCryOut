using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYServer.Models
{
    /// <summary>
    /// Compose helper for Uri parameters
    /// </summary>
    class UriParamsComposer : Dictionary<string, string>
    {
        public UriParamsComposer()
        {
        }

        public UriParamsComposer(Uri uri)
        {
            //var uriParams = uri.Query.Skip(1).ToString().Split('&');
            var uriParams = uri.Query.Remove(0, 1).Split('&');
            foreach (var p in uriParams)
            {
                var parts = p.Split('=');
                if (2 == parts.Length)
                    Add(parts[0], parts[1]);
            }
        }

        public new void Add(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) ||
                string.IsNullOrWhiteSpace(value))
                return;
            base.Add(key, value);
        }

        public override string ToString()
        {
            var uri = string.Empty;
            foreach (var param in this)
            {
                if (!string.IsNullOrWhiteSpace(uri))
                    uri += "&";
                uri += param.Key + "=" + param.Value;
            }
            return uri;
        }
    }
}