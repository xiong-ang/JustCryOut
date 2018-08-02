using CYServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Threading;

namespace CYServer.Controllers
{
    public class FileController : ApiController
    {
        [HttpGet]
        [Route("media")]
        public IHttpActionResult GetMediaFile(string FileName)
        {
            if (String.IsNullOrWhiteSpace(FileName))
                return FileNotFound();
            var FilePath = Media.Instance.MediaPath;
            var filePath = Path.Combine(FilePath, FileName);

            return !String.IsNullOrWhiteSpace(filePath) ? GetFile(filePath) : FileNotFound();
        }

        /// <summary>
        /// Return html page with 404 file not find
        /// </summary>
        /// <returns></returns>
        private IHttpActionResult FileNotFound()
        {
            return StreamResponse(null, MediaTypeHeaderValue.Parse("text/html;charset=iso-8859-1"), HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Create http response with specific file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private IHttpActionResult GetFile(string filePath)
        {
            HttpContent data;
            MediaTypeHeaderValue contentType = null;
            var extendedPath = Path.GetExtension(filePath);

            if (Request.Method == HttpMethod.Get && extendedPath != null)
            {
                data = new StreamContent(File.OpenRead(filePath));
                contentType = MediaTypeHeaderValue.Parse(MimeMapping.GetMimeMapping(extendedPath ?? string.Empty));
            }
            else
            {
                data = new ByteArrayContent(new byte[0]);
            }

            return StreamResponse(data, contentType);
        }

        /// <summary>
        /// Create http response with file stream
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private IHttpActionResult StreamResponse(HttpContent content, MediaTypeHeaderValue contentType, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(status)
            {
                Content = content
            };

            response.Content.Headers.ContentType = contentType;

            //Force web api to not add header cache-control: no-cache to response
            response.Headers.CacheControl = new CacheControlHeaderValue();

            return ResponseMessage(response);
        }
    }
}
