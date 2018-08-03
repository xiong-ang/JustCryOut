using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CYServer.Models
{
    public class Media
    {
        public readonly string MediaPath = ConfigManager.Instance.GetWebConfigValueByKey("DataPath");
        private AccessManager AM
        {
            get
            {
                return AccessManager.Instance;
            }
        }

        #region signalton
        private static Media _instance;
        private static Object MLock = new Object();
        public static Media Instance
        { 
            get
            {
                lock (MLock)
                {
                    return _instance ?? (_instance = new Media());
                }
                
            }
        }
        private Media()
        {
            AM.Login();
        }
        #endregion signalton

        #region DataProcess
        private static ConcurrentDictionary<string, string> MediaDict = new ConcurrentDictionary<string, string>();
        public string GetMediaPath(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return string.Empty;

            string mediaPath = string.Empty;
            if(!MediaDict.Keys.Contains(message)
             /*   || !File.Exists(Path.Combine(MediaPath, MediaDict[message]).ToString())*/)
            {
                //Get Media path
                mediaPath = WordToVedio(message);

                MediaDict.TryAdd(message, mediaPath);
            }
            return MediaDict[message];
        }
        #endregion DataProcess

        #region wordToVedio
        private string WordToVedio(string message)
        {
            //访问接口，下载MP3文件
            var uri = new UriBuilder(@"http://tsn.baidu.com/text2audio");
            uri.Query = new UriParamsComposer
            {
                {"lan","zh"},
                {"ctp","1"},
                {"cuid","abcdxxx"},
                {"tok",AM.AMToken.access_token},
                {"tex",message},
                {"vol","9"},
                {"per","5"},
                {"spd","5"},
                {"pit","5"},
                {"aue","3"}
            }
            .ToString();
            //AjaxHelper.GetCall(uri.ToString(), string.Empty, out answer);
            DownloadHelper helper = new DownloadHelper();
            var fileName = Guid.NewGuid() + ".mp3";
            helper.FileName = fileName;
            helper.Download(uri.ToString(), MediaPath);

            return fileName;
        }
        #endregion wordToVedio
    }
}