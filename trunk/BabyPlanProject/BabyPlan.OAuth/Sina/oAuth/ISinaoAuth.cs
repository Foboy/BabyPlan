using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialServices.Sina.oAuth
{
    /**
     * 新浪微博OAuth2认证所用接口
     * 新浪微博oAuth认证包含如下步骤:
     * 1.请求用户授权Token即:根据client_Id(类似appCode)和访客的credential(用户名和密码)换取Authorized code 
     * 2.获取授权过的Access Token:本应用采用grant_type为authorization_code     
     * */
    public interface ISinaoAuth
    {
        /// <summary>
        /// 结合必须参数生成获取用户授权的地址
        /// 返回结果
        /// 格式如：https://api.t.sina.com.cn/oauth2/authorize?client_id=123050457758183&redirect_uri=http://www.example.com/response&response_type=code
        ///同意授权后会重定向 http://www.example.com/response&code=CODE
        /// </summary>
        /// <returns></returns>
        string GetAuthorizeUri();        

        /// <summary>
        /// 获取Access_token
        /// </summary>
        /// <param name="code">第一步返回的authorized_code</param>    
        /// <param name="access_token">访问该应用的令牌</param>
        /// <param name="uid">用户id</param>
        /// <returns>true/false 获取access token 成功与否</returns>
        bool GetAccessToken(string code,out string access_token,out string uid);
    }
}
