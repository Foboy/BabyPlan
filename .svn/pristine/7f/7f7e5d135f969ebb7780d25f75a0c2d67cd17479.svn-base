using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWeiboSDK;

namespace BabyPlan.oAuth
{
   public class QQService:IQQService
   {
       public string Get_UnAuthorized_Token()
       {
           string url = "https://open.t.qq.com/cgi-bin/request_token";
           List<Parameter> parameters = new List<Parameter>();
           OauthKey oauthKey = new OauthKey();
           oauthKey.customKey = QQOAuthConfig.AppKey;
           oauthKey.customSecret = QQOAuthConfig.AppSecret;
           oauthKey.callbackUrl = QQOAuthConfig.CallBackURI;

           QWeiboRequest request = new QWeiboRequest();
            
           return request.SyncRequest(url, "GET", oauthKey, parameters, null);
       }
        
   }
}
