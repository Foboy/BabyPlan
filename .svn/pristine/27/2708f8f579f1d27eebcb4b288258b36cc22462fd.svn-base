using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents the unread counters info.
    /// </summary>
    [Serializable]
    [DataContract]
    public class UnreadInfo
    {
        /// <remarks/>
        [DataMember(Name = "status")]
        public int? NewStatuses { get; set; }

        /// <remarks/>
        [DataMember(Name="follower")]
        public int Followers { get; set; }

        /// <remarks/>
        [DataMember(Name="dm")]
        public int Messages { get; set; }

        /// <remarks/>
        [DataMember(Name = "cmt")]
        public int Comments { get; set; }

        /// <remarks/>
        [DataMember(Name = "mention_status")]
        public int MentionStatuses { get; set; }

        /// <remarks/>
        [DataMember(Name = "mention_cmt")]
        public int MentionComments { get; set; }

        /// <remarks/>
        [DataMember(Name = "group")]
        public int GroupMessages { get; set; }

        /// <remarks/>
        [DataMember(Name = "private_group")]
        public int PrivateGroupMessages { get; set; }

        /// <remarks/>
        [DataMember(Name = "notice")]
        public int Notices { get; set; }

        /// <remarks/>
        [DataMember(Name = "invite")]
        public int Invites { get; set; }

        /// <remarks/>
        [DataMember(Name = "badge")]
        public int Badges { get; set; }

        /// <remarks/>
        [DataMember(Name = "photo")]
        public int PhotoMessages { get; set; }


    }
}
