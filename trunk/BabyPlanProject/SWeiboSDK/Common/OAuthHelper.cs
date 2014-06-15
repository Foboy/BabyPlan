using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMicroblogAPI.Common
{
    /// <summary>
    /// Provides helper methods for OAuth purpose.
    /// </summary>
    public static class OAuthHelper
    {
        /// <summary>
        /// Constructs the OAuth authorization header with the specified parameters. See RFC5849 section 3.4.1.3.2.
        /// <remarks>
        /// This method does the following:
        /// 1. Creates signature-base-string with the specified <paramref name="uri"/>, <paramref name="parameters"/> and the <paramref name="httpMethod"/>.
        /// 2. Creates signature upon signature-base-string with sign-key (by <see cref="Environment.AppKey"/> and <paramref name="tokenSecret"/>).
        /// 3. Constructs OAuth Authentication header.
        /// </remarks>
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <param name="httpMethod">The HTTP methods, like POST, GET etc.</param>
        /// <param name="uri">The uri.</param>
        /// <param name="tokenSecret">The token secret. (Could be a request token secret or access token secret.)</param>
        /// <returns>The OAuth authorization header string.</returns>
        public static string ConstructOAuthHeader()
        {
            var headerBuilder = new StringBuilder();
            headerBuilder.Append(Constants.OAuthHeaderPrefix);
            headerBuilder.Append(Environment.AccessToken.Token);

            return headerBuilder.ToString();
        }

        /// <summary>
        /// Constructs the query string. 
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <returns>The query string.</returns>
        public static string ConstructQueryString(IEnumerable<ParamPair> parameters)
        {
            var queryStringBuilder = new StringBuilder();

            foreach (var item in parameters)
            {
                var name = RFC3986Encoder.Encode(item.Name);
                var val = RFC3986Encoder.Encode(item.Value);
                queryStringBuilder.Append(string.Format("{0}={1}&", name, val));
            }

            return queryStringBuilder.ToString();
        }


        /// <summary>
        /// Prepares a post body string for an access-token-required request.
        /// </summary>
        /// <param name="uri">The uri to identify the resource.</param>
        /// <param name="customPostParams">Additional parameters (in addition to the OAuth parameters) to be included in the post body.</param>
        /// <returns>The url-encoded post body string.</returns>
        public static string PreparePostBody(IEnumerable<ParamPair> customPostParams)
        {
            var parameters = new ParamCollection();
            //OAuthAccessToken accessToken = Environment.AccessToken;

            //parameters.Add(Constants.OAuthToken, accessToken.Token);

            if (null != customPostParams)
            {
                foreach (var item in customPostParams)
                {
                    parameters.Add(item.Name, item.Value);
                }
            }

            var postBody = ConstructPostBody(parameters);

            return postBody;
        }

        /// <summary>
        /// Constructs the OAuth string for the Http-Post request's body.
        /// </summary>
        /// <param name="parameters">The parameters to used to construct OAuth authorization header(including OAuth protocol params and request-specific params).</param>
        /// <param name="uri">The uri to identify the resource.</param>
        /// <param name="accessTokenSecret">The access token secret.</param>
        /// <returns>The OAuth string for a HTTP-Post body.</returns>
        private static string ConstructPostBody(IEnumerable<ParamPair> parameters)
        {
            var bodyBuilder = new StringBuilder();
            foreach (var item in parameters)
            {
                var name = Uri.EscapeDataString(item.Name);//RFC3986Encoder.Encode(item.Name);
                var val = Uri.EscapeDataString(item.Value); ;//RFC3986Encoder.Encode(item.Value);
                bodyBuilder.Append(string.Format("{0}={1}&", name, val));
            }

            var result = bodyBuilder.ToString().TrimEnd('&');

            return result;
        }
    }
}
