using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BabyPlan.mvcApp.UrlRoutes
{
    public class UrlRequestParameter
    {
        private static UrlRequestParameter requestParameter = new UrlRequestParameter();
        /// <summary>
        /// 使用当前请求Url中的值
        /// </summary>
        public static UrlRequestParameter RequestParameter
        {
            get
            {
                return requestParameter;
            }
        }
    }
}