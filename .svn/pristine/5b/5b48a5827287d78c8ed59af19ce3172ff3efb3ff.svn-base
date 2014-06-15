using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMicroblogAPI.Common;
using System.IO;

namespace AMicroblogAPI.HttpRequests
{
    public class ObtainTokenPost : HttpPost
    {
        public ObtainTokenPost(string uri)
            : base(uri)
        { }

        protected override string ConstructUri()
        {
            var uri = Uri;
            if (null != Params)
            {
                uri += "?";
                foreach (var item in Params)
                {
                    uri += string.Format("{0}={1}&", RFC3986Encoder.UrlEncode(item.Name), RFC3986Encoder.UrlEncode(item.Value));
                }
                uri = uri.TrimEnd('&');
            }

            return uri;
        }
    }
}
