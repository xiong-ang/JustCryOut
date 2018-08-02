using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CYServer.Models
{
    class DownloadHelper
    {
        public byte Process { get; set; }
        public string FileName { get; set; }
        public void Download(string link, string location)
        {
            Process = 0;
            //FileName = string.Empty;
            using (var wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProcessChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;

                var uri = new Uri(link);
                if (uri.IsLoopback) throw new Exception("Wrong uri");

                //FileName = Path.GetFileName(uri.LocalPath);
                var downloadedFileName = Path.Combine(location, FileName);
                wc.DownloadFile(uri, downloadedFileName);
                //wc.DownloadFileTaskAsync(uri, downloadedFileName).Wait();
            }
            Process = 100;
        }

        private void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine(FileName + ": Download Completed!");
        }

        private void wc_DownloadProcessChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Process = Convert.ToByte(e.ProgressPercentage);
            Console.WriteLine(FileName + ": " + Process);
        }
    }
}