using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QConnectSDK.Models;

namespace BabyPlan.oAuth
{
    public class QoAuthKey
    {
        private string state;

        private string openId;

        private string access_token;

        public string State
        {
            get
            {
                return state;
            }
        }

        public string OpenId
        {
            get
            {
                return openId;
            }
        }

        public string Access_Token
        {
            get
            {
                return access_token;
            }
        }

        public QoAuthKey()
        {
            state = Guid.NewGuid().ToString().Replace("-", "");
        }

        public void ParseToken(OAuthToken token)
        {
            this.openId = token.OpenId;
            this.access_token = token.AccessToken;
        }
    }
}
