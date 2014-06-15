using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWeiboSDK;
using System.Web;
using QConnectSDK.Context;
using QConnectSDK;

namespace BabyPlan.oAuth
{
    public class QoAuthService : IQoAuth
    {

        #region IQoAuth 成员

        public string GetAuthorizationUrl(out QoAuthKey oAuthKey)
        {
            
            string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            oAuthKey = new QoAuthKey();
            QzoneContext context = new QzoneContext();
            context.Config.GetAppSecret();
            return context.GetAuthorizationUrl(oAuthKey.State, scope);
        }

        public void GetAccessToken(string verifier, ref QoAuthKey oAuthKey)
        {
            QOpenClient qzone = new QOpenClient(verifier, oAuthKey.State);
            oAuthKey.ParseToken(qzone.OAuthToken);
        }

        #endregion
    }
}
