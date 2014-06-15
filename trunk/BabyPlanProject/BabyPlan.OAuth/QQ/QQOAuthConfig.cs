using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;

namespace BabyPlan.oAuth
{
    public class QQOAuthConfig
    {
        private static NameValueCollection QQSection = (NameValueCollection)ConfigurationManager.GetSection(@"QQSectionGroup/QQSection");

        /// <summary>
        /// 申请的应用Key
        /// </summary>
        public static readonly string AppKey = QQSection["AppKey"];

        /// <summary>
        /// 签名值，密钥为：App Secret 
        /// </summary>
        public static readonly string AppSecret = QQSection["AppSecret"];

        /// <summary>
        /// 认证成功后浏览器会被重定向到的url
        /// </summary>
        public static readonly string CallBackURI = QQSection["CallBackURI"];
    }
}
