using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a comment.
    /// </summary>
    [Serializable]
    [DataContract]
    public class CommentInfo
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
        /// Gets or sets the user who posts this comment.
        /// </summary>
        [DataMember(Name="user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the status which this comment comments to.
        /// </summary>
        [DataMember(Name="status")]
        public StatusInfo Status{ get; set; }
    }
}
