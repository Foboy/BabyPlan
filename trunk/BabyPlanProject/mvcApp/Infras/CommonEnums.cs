using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyPlan.mvcApp.Infras
{
    /// <summary>
    /// 用户登录方式    
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 采用新浪微博方式登录
        /// </summary>
        FromSina = 0,

        /// <summary>
        /// 采用腾讯微博账号登陆
        /// </summary>
        FromQQ = 1,

        /// <summary>
        /// 平台账号登陆
        /// </summary>
        FromSystemUser = 2
    }
}