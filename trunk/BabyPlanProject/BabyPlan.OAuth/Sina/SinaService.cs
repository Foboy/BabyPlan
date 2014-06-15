using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialServices.Sina.oAuth;
using AMicroblogAPI;
using AMicroblogAPI.DataContract;
using BabyPlan.oAuth.Common;


namespace BabyPlan.oAuth
{
    
   public class SinaService:ISinaService
    {

       public string Users_Show(string access_token, string uid)
        {
            string uri = "https://api.weibo.com/2/users/show.json";
            List<OAuthParameter> list = new List<OAuthParameter>();
            list.Add(new OAuthParameter("access_token",access_token));
            list.Add(new OAuthParameter("uid",uid));
            return SinaOAuthUtil.HttpGet(uri, SinaOAuthUtil.GetQueryStringFromParameters(list));
        }
 
 
 

 

 
    }
}
