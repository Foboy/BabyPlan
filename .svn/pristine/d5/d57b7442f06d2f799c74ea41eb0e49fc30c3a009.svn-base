using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;

namespace SocialServices.Sina.oAuth
{
    /// <summary>
    /// 新浪OAuth2.0的配置信息
    /// </summary>
    internal class SinaConfig
    {
        private static NameValueCollection SinaSection = (NameValueCollection)ConfigurationManager.GetSection(@"SinaSectionGroup/SinaSection");

        public static readonly string ClientId = SinaSection["AppKey"];

        public static readonly string ClientSecret = SinaSection["AppSecret"];

        public static readonly string RedirectUri = SinaSection["CallBackURI"];
         
         
         
        
    }
}
