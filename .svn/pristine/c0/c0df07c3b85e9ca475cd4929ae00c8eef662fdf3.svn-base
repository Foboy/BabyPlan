using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using BabyPlan.oAuth.Common;

namespace SocialServices.Sina.oAuth
{
   /// <summary>
   /// 认证中需要的方法
   /// </summary>
   internal static class SinaOAuthUtil
    {
       public static string GetQueryStringFromParameters(List<OAuthParameter> list)
       {
           if (list == null || list.Count == 0)
               return "";
           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < list.Count; i++)
           {
               //string str = i == 0 ? "?{0}={1}" : "&{0}={1}";
               string str = i == 0 ? "{0}={1}" : "&{0}={1}";
               sb.AppendFormat(str, list[i].ParameterName, list[i].ParameterValue);
           }
           return sb.ToString();
       }
       /// <summary>
       /// json数据转对象
       /// </summary>
       /// <param name="strJson">json数据</param>
       /// <returns></returns>
       public static T ParseJson<T>(string strJson)
       {
           JavaScriptSerializer serializer = new JavaScriptSerializer();
           return serializer.Deserialize<T>(strJson);
       }
       /// <summary>
       /// 同步方式发起http post请求
       /// </summary>
       /// <param name="url">请求URL</param>
       /// <param name="queryString">参数字符串</param>
       /// <returns>请求返回值</returns>
       public static string HttpPost(string url, string queryString)
       {
           StreamWriter requestWriter = null;
           string responseData = null;
           HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
           webRequest.Method = "POST";
           webRequest.ContentType = "application/x-www-form-urlencoded";
           webRequest.ServicePoint.Expect100Continue = false;
           webRequest.Timeout = 20000;
           webRequest.KeepAlive = false;
           try
           {
               //POST the data.
               requestWriter = new StreamWriter(webRequest.GetRequestStream());
               requestWriter.Write(queryString);
           }
           catch
           {
               throw;
           }
           finally
           {
               requestWriter.Close();
               requestWriter = null;
           }

           try
           {
               responseData = WebResponseGet(webRequest);

               webRequest = null;

               return responseData;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       /// <summary>
       /// 获取返回结果http get请求
       /// </summary>
       /// <param name="webRequest">webRequest对象</param>
       /// <returns>请求返回值</returns>
       public static string WebResponseGet(HttpWebRequest webRequest)
       {
           try
           {
               HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
               StreamReader responseReader = null;
               string responseData = String.Empty;
               responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
               responseData = responseReader.ReadToEnd();

               webRequest.GetResponse().GetResponseStream().Close();
               responseReader.Close();
               responseReader = null;
               return responseData;
           }
           catch (WebException wex)
           {
               string responseData = String.Empty;
               if (wex.Status == WebExceptionStatus.ProtocolError)
               {
                   try
                   {
                       responseData = String.Empty;
                       StreamReader responseReader = new StreamReader(wex.Response.GetResponseStream());
                       responseData = responseReader.ReadToEnd();
                       responseReader.Close();
                       responseReader = null;
                   }
                   catch { }
               }
               if (!string.IsNullOrEmpty(responseData))
               {
           
                   throw new WebException(responseData);
               }
               throw wex;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
     /// <summary>
       /// 获取返回结果http get请求
     /// </summary>
     /// <param name="url"></param>
     /// <returns></returns>
       public static string HttpGet(string url, string queryString)
       {
           string responseData = null;

           if (!string.IsNullOrEmpty(queryString))
           {
               url += "?" + queryString.Trim(' ', '?', '&');
           }

           HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
           webRequest.Method = "GET";
           webRequest.ServicePoint.Expect100Continue = false;
           webRequest.Timeout = 20000;
           webRequest.KeepAlive = false;

           try
           {
               responseData = WebResponseGet(webRequest);

               webRequest = null;

               return responseData;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
