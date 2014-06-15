using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyPlan.mvcApp.Infras
{
    //自定义Session来避免ASP.NET中的Session的种种问题
    //1.ASP.NET中Session对并发的影响
    //2.ASP.NET中Session不稳定
    //暂时不做线程安全问题处理
    public class SessionEx
    {
        public static Dictionary<string, Dictionary<string, dynamic>> GlobalCache = new Dictionary<string, Dictionary<string, dynamic>>();

        /// <summary>
        /// 为每个客户端生成一个唯一的SessionKey
        /// </summary>
        /// <returns></returns>
        public static string GetSessionKey()
        {
            //格式:102447989a344245b1ef9143f9b1e68a
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 设置全局缓存
        /// </summary>
        /// <param name="sessionKey">客户端请求的sessionKey</param>
        /// <param name="dicKey">保存值的Key</param>
        /// <param name="obj">保存的动态对象</param>
        public static void SetCache(string sessionKey,string dicKey,dynamic obj)
        {
            bool isContain = GlobalCache.ContainsKey(sessionKey);
            Dictionary<string, dynamic> tempDic = null;
            if (!isContain)             
                tempDic = new Dictionary<string, dynamic>(); 
            else
               tempDic= GlobalCache[sessionKey]; 
            tempDic.Add(dicKey, obj);
            GlobalCache.Add(sessionKey,tempDic);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="sessionKey">客户端请求的sessionKey</param>
        /// <param name="dicKey">缓存的Key</param>
        /// <param name="clearFlag">是否清除该缓存</param>
        /// <returns></returns>
        public static dynamic GetCache(string sessionKey, string dicKey,bool clearFlag)
        {
            //是否为当前请求的客户分配缓存内存
            bool isContainsKey = GlobalCache.ContainsKey(sessionKey);

            //是否存在该缓存键值
            if (isContainsKey)
                isContainsKey = GlobalCache[sessionKey].ContainsKey(dicKey); 

            if(isContainsKey)
            {
                dynamic  obj = GlobalCache[sessionKey][dicKey];
                if(clearFlag)
                    GlobalCache[sessionKey].Remove(dicKey);
                return obj;
            }   
            else 
               return null;
        }
    }
}