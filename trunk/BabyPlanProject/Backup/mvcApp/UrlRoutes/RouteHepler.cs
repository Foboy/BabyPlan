using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BabyPlan.mvcApp.UrlRoutes
{
    public class RouteHepler
    {
        /// <summary>
        /// 是否使用当前请求Url中的参数值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsRequestRouteValue(RouteValueDictionary values, string key)
        {
            if (values.ContainsKey(key))
            {
                return values[key] is UrlRequestParameter;
            }
            else
            {
                return false;
            }
        }

        public static bool TryGetRouteValue<T>(RouteValueDictionary values, string key, out T value, T defaultValue)
        {
            if (values.ContainsKey(key))
            {
                try
                {
                    value = (T)values[key];
                    return true;
                }
                catch
                {
                    value = defaultValue;
                    return false;
                }
            }
            else
            {
                value = defaultValue;
                return false;
            }
        }

        public static void ParseRouteValue<T>(RouteValueDictionary requestValues, RouteValueDictionary values,string key,out T value,T defalutValue)
        {
            if (RouteHepler.IsRequestRouteValue(values, key))
            {
                RouteHepler.TryGetRouteValue<T>(requestValues, key, out value, defalutValue);
            }
            else
            {
                RouteHepler.TryGetRouteValue<T>(values, key, out value, defalutValue);
            }
        }
    }
}