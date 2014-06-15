using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BabyPlan.TaoBao
{
    /// <summary>
    /// Summary description for Configure
    /// </summary>
    public class Configure
    {
        private static Configure configInstance = null;
        private String appKey;
        public String AppKey
        {
            get
            {
                return appKey;
            }
            set
            {
                appKey = value;
            }
        }

        private String appSecret;
        public String AppSecret
        {
            get
            {
                return appSecret;
            }
            set
            {
                appSecret = value;
            }
        }
        private String channelUrl;
        public String ChannelUrl
        {
            get
            {
                return channelUrl;
            }
            set
            {
                channelUrl = value;
            }
        }
        private Configure()
        {

        }

        public static Configure LoadConfig(String sConfigPath)
        {
            if (configInstance == null)
            {
                configInstance = new Configure();
                loadConfigure(sConfigPath);
            }
            return configInstance;
        }
        private static void loadConfigure(String sConfigPath)
        {
            XmlDocument XmlConfig = new XmlDocument();
            try
            {
                XmlConfig.Load(sConfigPath);
            }
            catch
            {

            }
            XmlNodeList XmlHostList;
            XmlHostList = XmlConfig.GetElementsByTagName("appConfig");
            if (XmlHostList.Count > 0)
            {
                int j = 0;
                for (j = 0; j < XmlHostList[0].ChildNodes.Count; j++)
                {
                    if ("appkey".Equals(XmlHostList[0].ChildNodes[j].Name))
                    {
                        configInstance.appKey = XmlHostList[0].ChildNodes[j].InnerText.Trim();
                    }
                    else if ("secret".Equals(XmlHostList[0].ChildNodes[j].Name))
                    {
                        configInstance.appSecret = XmlHostList[0].ChildNodes[j].InnerText.Trim();
                    }
                    else if ("channelUrl".Equals(XmlHostList[0].ChildNodes[j].Name))
                    {
                        configInstance.channelUrl = XmlHostList[0].ChildNodes[j].InnerText.Trim();
                    }
                }
            }
        }
    }
}
