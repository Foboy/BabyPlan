using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a relationship part.
    /// </summary>
    [Serializable]
    [DataContract]
    public class RelationshipPart
    {
        /// <remarks/>
        [DataMember(Name="id")]
        public long UserID { get; set; }

        /// <remarks/>
        [DataMember(Name="screen_name")]
        public string ScreenName { get; set; }

        /// <remarks/>
        [DataMember(Name="following")]
        public bool Following { get; set; }

        /// <remarks/>
        [DataMember(Name="followed_by")]
        public bool FollowedBy { get; set; }

        /// <remarks/>
        [DataMember(Name="notifications_enabled")]
        public bool NotificationsEnabled { get; set; }

    }
}
