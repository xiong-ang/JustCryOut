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
            //访问https://openapi.baidu.com/oauth/2.0/token 换取 token
            var uri = new UriBuilder(@"https://openapi.baidu.com/oauth/2.0/token");
            uri.Query = new UriParamsComposer
            {
                {"grant_type","client_credentials"},
                {"client_id","PIX9PCkB41qRmmopUcFi2CrY"},
                {"client_secret","rDSRUf2GCHh5H9oMHdmH1cYgNV4WYPYL"}
            }
            .ToString();
            string answer;
            AjaxHelper.GetCall(uri.ToString(), string.Empty, out answer);
            Token tk = JsonHelper.FormJson<Token>(answer);

            //访问接口，下载MP3文件
            uri = new UriBuilder(@"http://tsn.baidu.com/text2audio");
            uri.Query = new UriParamsComposer
            {
                {"lan","zh"},
                {"ctp","1"},
                {"cuid","abcdxxx"},
                {"tok",tk.access_token},
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

            //Return until download complete
            /*
            while (!File.Exists(Path.Combine(MediaPath, fileName)))
            {
                Thread.Sleep(100);
            }
            */
            return fileName;
        }
        #endregion wordToVedio
    }
}