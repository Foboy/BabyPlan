using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMicroblogAPI;
using AMicroblogAPI.Common;


namespace SocialServices.Sina.oAuth
{
    public class SinaOAuthService:ISinaoAuth
    {
        public string GetAuthorizeUri()
        {
            return AMicroblog.GetAuthorizationUri(SinaConfig.RedirectUri);
            //string uri = "https://api.weibo.com/oauth2/authorize";
            //List<OAuthParameter> pars = new List<OAuthParameter>();
            //pars.Add(new OAuthParameter("client_id", SinaConfig.ClientId));
            //pars.Add(new OAuthParameter("response_type", "code"));
            //pars.Add(new OAuthParameter("redirect_uri",SinaConfig.RedirectUri));
            //uri = uri + "?" + SinaOAuthUtil.GetQueryStringFromParameters(pars);
            //return uri;
        }

        public bool GetAccessToken(string code, out string access_token,out string uid)
        {
            bool is_ok = false;
            access_token = string.Empty;
            uid = string.Empty;
            try
            {
                OAuthAccessToken token = AMicroblog.GetAccessTokenByAuthorizationCode(code, SinaConfig.RedirectUri);
                is_ok = true;
                access_token = token.Token;
                uid = token.UserID;
            }
            catch { }
            return is_ok;
           //access_token = string.Empty;
           //uid = string.Empty;
           //string uri = "https://api.weibo.com/oauth2/access_token";           
           //List<OAuthParameter> list = new List<OAuthParameter>();
           //list.Add(new OAuthParameter("client_id", SinaConfig.ClientId));
           //list.Add(new OAuthParameter("client_secret",SinaConfig.ClientSecret));
           //list.Add(new OAuthParameter("grant_type", GrantTypeEnum.authorization_code.ToString()));
           //list.Add(new OAuthParameter("code", code));  
           //list.Add(new OAuthParameter("redirect_uri", SinaConfig.RedirectUri));
           //bool is_ok = false;
           //try
           //{
           //    string result = SinaOAuthUtil.HttpPost(uri, SinaOAuthUtil.GetQueryStringFromParameters(list));

           //    Dictionary<string, object> dic = SinaOAuthUtil.ParseJson<Dictionary<string, object>>(result);
           //    access_token = dic["access_token"].ToString();
           //    uid = dic["uid"].ToString();
           //    is_ok = true;
           //}
           //catch { }
           return is_ok;
        }
    }
}
