using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyPlan.oAuth
{
    /*     
     *定义腾讯微博接口 
     * */
    public interface IQQService
    {
        /**
         * 认证相关(OAuth认证标准)
         * 认证流程：
         * 1.获取未授权的Request Token(temporary credentials)
         * 2.请求用户授权Request Token
         * 3.使用授权后的Request Token换取Access Token(token credentials)
         * 4.使用 Access Token 访问或修改受保护资源
         * */
        #region 认证相关
        //认证流程1:获取未授权的Request Token(temporary credentials)
        //返回结果:格式oauth_token=hdk48Djdsa&oauth_token_secret=xyz4992k83j47x0b&oauth_callback_confirmed=true
        string Get_UnAuthorized_Token();

        //认证流程2:请求用户授权Request Token
        //返回结果：格式oauth_token=hdk48Djdsa&oauth_verifier=473f82d3
        //string Get_Authorized_Token(string oauth_token);

        //认证流程3:使用授权后的Request Token换取Access Token(token credentials)
        //返回结果：格式oauth_token=nnch734d00ls2jdk&oauth_token_secreate=pdkkdhi9sl3r4s00
        //string Get_Access_Token();
        #endregion

    }
}
