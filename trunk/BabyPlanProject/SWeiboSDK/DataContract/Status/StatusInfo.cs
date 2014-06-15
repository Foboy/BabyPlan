using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents the status info.
    /// </summary>
    [Serializable]
    [DataContract]
    public class StatusInfo
    {
        /// <summary>
        /// Gets or sets the creation time of the status.
        /// </summary>
        [DataMember(Name="created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the stutus id.
        /// </summary>
        [DataMember(Name="id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        [DataMember(Name="text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the source of the status.
        /// </summary>
        [DataMember(Name="source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets a boolead indicates whether the status is favarited.
        /// </summary>
        [DataMember(Name="favorited")]
        public bool Favorited { get; set; }

        /// <summary>
        /// Gets or sets a boolead indicates whether the status is truncated.
        /// </summary>
        [DataMember(Name="truncated")]
        public bool Truncated { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [DataMember(Name="in_reply_to_status_id")]
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [DataMember(Name="in_reply_to_user_id")]
        public string ReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [DataMember(Name="in_reply_to_screen_name")]
        public string ReplyToUserScreenName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail_pic.
        /// </summary>
        [DataMember(Name="thumbnail_pic")]
        public string ThumbnailPic { get; set; }

        /// <summary>
        /// Gets or sets the bmiddle_pic.
        /// </summary>
        [DataMember(Name="bmiddle_pic")]
        public string MiddlePic { get; set; }

        /// <summary>
        /// Gets or sets the original_pic.
        /// </summary>
        [DataMember(Name="original_pic")]
        public string OriginalPic { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [DataMember(Name="mid")]
        public string Mid { get; set; }

        /// <summary>
        /// Gets or sets the user who posts this status.
        /// </summary>
        [DataMember(Name="user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the user who posts this status.
        /// </summary>
        [DataMember(Name="geo")]
        public Geo Geo { get; set; }

        /// <summary>
        /// Gets or sets the status that current status is reposted with.
        /// </summary>
        [DataMember(Name="retweeted_status")]
        public StatusInfo RetweetedStatus { get; set; }
    }
}
