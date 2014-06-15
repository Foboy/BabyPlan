using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a short and long url mapping.
    /// </summary>
    [Serializable]
    [DataContract]
    public class UrlInfo
    {
        /// <summary>
        /// Gets or sets the short url.
        /// </summary>
        [DataMember(Name="url_short")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// Gets or sets the long url.
        /// </summary>
        [DataMember(Name="url_long")]
        public string LongUrl { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name="type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the share count of the short url.
        /// </summary>
        [DataMember(Name="share_counts")]
        public int SharedCount { get; set; }

        /// <summary>
        /// Gets or sets the comment count of the short url.
        /// </summary>
        [DataMember(Name="comment_counts")]
        public int CommentCount { get; set; }
    }
}
