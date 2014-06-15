using System;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace AMicroblogAPI.Common
{
    /// <summary>
    /// Represents the OAuth access token.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("AccessToken:{Token}")]
    [DataContract]
    public class OAuthAccessToken
    {
        /// <summary>
        /// Gets or sets the token field.
        /// </summary>
        [DataMember(Name = "access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token field.
        /// </summary>
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the user id field.
        /// </summary>
        [DataMember(Name = "uid")]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the expire in field.
        /// </summary>
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}
