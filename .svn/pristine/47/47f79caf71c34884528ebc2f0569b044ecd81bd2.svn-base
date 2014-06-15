using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWeiboSDK;

namespace BabyPlan.oAuth
{
    public class QTokenHelper
    {
        /// <summary>
        /// 解析腾讯微博认证过程中返回的结果
        /// 认证结果1.oauth_token=hdk48Djdsa&oauth_token_secret=xyz4992k83j47x0b&oauth_callback_confirmed=true
        /// 认证结果2.oauth_token=hdk48Djdsa&oauth_verifier=473f82d3
        /// 认证结果3.oauth_token=nnch734d00ls2jdk&oauth_token_secreate=pdkkdhi9sl3r4s00
        /// </summary>
        /// <param name="stepNum">认证步骤(1,2,3)</param>
        /// <param name="response">认证返回结果</param>
        /// <param name="oAuthkey">后续步骤所需参数对象</param>
        /// <returns></returns>
        public static bool ParseToken(int stepNum, string response, ref OauthKey oAuthkey)
        {
            //解析是否成功
            bool isParseSuccess = false;

            if (string.IsNullOrEmpty(response))
            {
                return isParseSuccess;
            }

            string[] retArr = response.Split('&');
            int retArrLen = retArr.Length;
            if (retArrLen < 2)
            {
                return isParseSuccess;
            }

            //解析Step1 返回的结果
            if (stepNum == 1)
            {                
                //oauth_token
                string[] arr_token_1 = retArr[0].Split('=');
                oAuthkey.tokenKey = arr_token_1[1];

                //oauth_token_secret
                string[] arr_token_secret_1 = retArr[1].Split('=');
                oAuthkey.tokenSecret = arr_token_secret_1[1];

                isParseSuccess = true;

            }
            else if (stepNum == 2) //解析Step2 返回的结果
            {
                 
                 //oauth_token
                string[] arr_token_2 = retArr[2].Split('=');
                oAuthkey.tokenKey = arr_token_2[1];

                //oauth_token_secret
                string[] verify_arr = retArr[3].Split('=');
                oAuthkey.verify = verify_arr[1];
                isParseSuccess = true;
             }
            else if (stepNum == 3) //解析Step3 返回的结果
            {
                string[] arr_token_3 = retArr[0].Split('=');
                oAuthkey.tokenKey = arr_token_3[1];

                //oauth_token_secret
                string[] arr_token_secret_3 = retArr[1].Split('=');
                oAuthkey.tokenSecret = arr_token_secret_3[1];
                isParseSuccess = true;
            }

            return isParseSuccess;
        }
    }
}
