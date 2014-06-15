﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AMicroblogAPI.Common;

namespace AMicroblogAPI.HttpRequests
{
    /// <summary>
    /// Performs a OAuth token-attached Http-Post request to the resource identified by the uri with the combination of OAuth parameters and the specified additionalParams(optional).
    /// </summary>
    public class OAuthHttpPost : HttpPost
    {
       // private IList<ParamPair> additionalParams = new Collection<ParamPair>();
        private ParamCollection postParams = new ParamCollection();

        private OAuthAccessToken accessToken = null;

        /// <summary>
        /// Initializes a new instance of <see cref="OAuthHttpPost"/> with the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The uri to identify a resource in the remote server.</param>
        public OAuthHttpPost(string uri) : base(uri)
        { 
            
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OAuthHttpPost"/> with the specified <paramref name="uri"/> and <paramref name="additionalParams"/>.
        /// </summary>
        /// <param name="uri">The uri to identify a resource in the remote server.</param>
        /// <param name="additionalParams">Additional data to post.</param>
        public OAuthHttpPost(string uri, IList<ParamPair> additionalParams, OAuthAccessToken token = null)
            : this(uri)
        {
            accessToken = token;
            foreach (var item in additionalParams)
                this.postParams.Add(item);
        }

        /// <summary>
        /// Gets or sets the custom post data(in <c>ParamPair</c> format, non-urlencoded).
        /// <remarks>Do not UrlEncode the <c>ParamPair</c>, it is done inside this method.</remarks>
        /// </summary>
        public ParamCollection Params
        {
            get
            {
                return postParams;
            }
        }

        /// <summary>
        /// Gets the data to post.
        /// </summary>
        public sealed override string PostData
        {
            get
            {
                OAuthAccessToken accessToken = this.accessToken ?? Environment.AccessToken;

                postParams.Add(Constants.OAuthToken, accessToken.Token);

                return OAuthHelper.PreparePostBody(postParams);
            }
            set
            {
                throw new NotSupportedException("Not supported. Please use CustomData property to convey data to post.");                
            }
        }
    }
}
