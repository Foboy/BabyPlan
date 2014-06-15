﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.ObjectModel;
using AMicroblogAPI.Common;

namespace AMicroblogAPI.HttpRequests
{
    /// <summary>
    /// Posts the multi-part fields in the post body with the OAuth authorization header in the request.
    /// </summary>
    public class OAuthMultiPartHttpPost : MultiPartHttpPost
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OAuthMultiPartHttpPost"/> with the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The uri to identify a resource in the remote server.</param>
        public OAuthMultiPartHttpPost(string uri)
            : base(uri)
        {
            
        }

        /// <summary>
        /// See <see cref="HttpRequest.AppendHeaders"/>.
        /// </summary>        
        protected override void AppendHeaders(WebHeaderCollection headers)
        {
            var oAuthHeader = OAuthHelper.ConstructOAuthHeader();
            
            headers.Add(HttpRequestHeader.Authorization, oAuthHeader);

            base.AppendHeaders(headers);
        }
    }
}
