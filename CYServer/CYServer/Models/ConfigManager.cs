using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CYServer.Models
{
    public class ConfigManager
    {
        #region signalton
        private static ConfigManager _instance;
        private static object SLock = new object();
        public static ConfigManager Instance
        {
            get
            {
                lock (SLock)
                {
                    return _instance ?? (_instance = new ConfigManager());
                }
                
            }
        }
        private ConfigManager() { }
        #endregion signalton


        #region Manage configuration
        public string GetAppConfig(string strKey)
        {
            //string file = System.Windows.Forms.Application.ExecutablePath;
            string file = AppDomain.CurrentDomain.BaseDirectory;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }

        public void UpdateAppConfig(string newKey, string newValue)
        {
            //string file = System.Windows.Forms.Application.ExecutablePath;
            string file = AppDomain.CurrentDomain.BaseDirectory;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetWebConfigValueByKey(string key)
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            if (appSetting.Settings[key] != null)//如果不存在此节点，则添加  
            {
                return appSetting.Settings[key].Value;
            }
            return null;
        }

        public void UpdateWebConfigValueByKey(string newKey, string newValue)
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion Manage configuration
    }
}