using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWeiboSDK;

namespace BabyPlan.oAuth
{
    /// <summary>
    /// 实现QQ提供的oAuth认证接口
    /// </summary>
    public interface IQoAuth
    {
        string GetAuthorizationUrl(out QoAuthKey oAuthKey);

        void GetAccessToken(string verifier, ref QoAuthKey oAuthKey);

    }
}
