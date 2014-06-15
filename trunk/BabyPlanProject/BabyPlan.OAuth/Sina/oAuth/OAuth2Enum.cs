using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialServices.Sina.oAuth
{
    /// <summary>
    /// 支持的值包括 code 和token 默认值为code
    /// </summary>
    public enum ResponseTypeEnum
    {
        /// <summary>
        /// response_type为code
        /// </summary>
        code,
        /// <summary>
        /// response_type为token
        /// </summary>
        token
    }
    /// <summary>
    /// 请求的类型,可以为:authorization_code ,password,refresh_token
    /// </summary>
    public enum GrantTypeEnum
    {
        /// <summary>
        /// grant_type为authorization_code时
        /// </summary>
        authorization_code,
        /// <summary>
        /// grant_type为password时
        /// </summary>
        password,
        /// <summary>
        /// grant_type为refresh_token时
        /// </summary>
        refresh_token
    }
    /// <summary>
    /// 授权页面类型 可选范围
    /// </summary>
    public enum DisplayEnum
    {
        /// <summary>
        /// 默认授权页面
        /// </summary>
        Default,
        /// <summary>
        /// 支持html5的手机
        /// </summary>
        Mobile,
        /// <summary>
        /// 弹窗授权页
        /// </summary>
        Popup,
        /// <summary>
        /// wap1.2页面
        /// </summary>
        Wap12,
        /// <summary>
        /// 	wap2.0页面	
        /// </summary>
        Wap20,
        /// <summary>
        /// js-sdk 专用 授权页面是弹窗，返回结果为js-sdk回掉函数		
        /// </summary>
        Js,
        /// <summary>
        /// 站内应用专用,站内应用不传display参数,并且response_type为token时,默认使用改display.授权后不会返回access_token，只是输出js刷新站内应用父框架
        /// </summary>
        Apponweibo

    }
}
