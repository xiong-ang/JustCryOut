using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using CYServer.Models;

namespace CYServer.Controllers
{
    public class DataController : ApiController
    {
        [HttpPost]
        [Route("cryout")]
        public string GetMedia(string message)
        {
            return Media.Instance.GetMediaPath(message);
        }
    }
}
