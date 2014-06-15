using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using BabyPlan.Common;

namespace BabyPlan.mvcApp.Configuration
{
    public class Config
    {
        private static Config instance;
        private static object state = new object();

        public string RegisteredTwitter { get; private set; }

        public AdUser SysUser { get; private set; }

        private Config()
        {
            try
            {
                RegisteredTwitter = GetAppSettings("RegisteredTwitter", string.Empty);
                string sysAccount = GetAppSettings("SysAccount", string.Empty);
                if (!string.IsNullOrEmpty(sysAccount))
                {
                    UserServiceClient client = new UserServiceClient();
                    AdvancedResult<AdUser> result = client.GetUserInfoByAccount(sysAccount);
                    if (result.Error == DataStructure.AppError.ERROR_SUCCESS)
                        SysUser = result.Data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetAppSettings(string nodeName, int min, int max)
        {
            int value = 0;
            int.TryParse(ConfigurationManager.AppSettings[nodeName], out value);
            if (value < min)
                value = min;
            if (value > max)
                value = max;
            return value;
        }

        private string GetAppSettings(string nodeName, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[nodeName];
            if (value == null)
                value = defaultValue;
            return value;
        }

        private bool GetAppSettings(string nodeName, bool defaultValue)
        {
            bool result = defaultValue;
            string value = ConfigurationManager.AppSettings[nodeName];
            if (value != null)
            {
                result = Convert.ToBoolean(value);
            }
            return result;
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (state)
                    {
                        if (instance == null)
                        {
                            instance = new Config();
                        }
                    }
                }
                return instance;
            }
        }
    }
}