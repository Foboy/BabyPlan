using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("User:{ScreenName}")]
    [DataContract]
    public class UserInfo
    {
        /// <remarks/>
        [DataMember(Name="id")]
        public long ID { get; set; }

        /// <remarks/>
        [DataMember(Name="screen_name")]
        public string ScreenName { get; set; }

        /// <remarks/>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the markup(note) you set for this user.
        /// </summary>
        [DataMember(Name="define_as")]
        public string DefineAs { get; set; }

        /// <remarks/>
        [DataMember(Name="province")]
        public string Province { get; set; }

        /// <remarks/>
        [DataMember(Name="city")]
        public string City { get; set; }

        /// <remarks/>
        [DataMember(Name="location")]
        public string Location { get; set; }

        /// <remarks/>
        [DataMember(Name="description")]
        public string Description { get; set; }

        /// <remarks/>
        [DataMember(Name="url")]
        public string Url { get; set; }

        /// <remarks/>
        [DataMember(Name="profile_image_url")]
        public string ProfileImageUrl { get; set; }

        /// <remarks/>
        [DataMember(Name="domain")]
        public string Domain { get; set; }

        /// <remarks/>
        [DataMember(Name="gender")]
        public string Gender { get; set; }

        /// <remarks/>
        [DataMember(Name="followers_count")]
        public int FollowersCount { get; set; }

        /// <remarks/>
        [DataMember(Name="friends_count")]
        public int FriendsCount { get; set; }

        /// <remarks/>
        [DataMember(Name="statuses_count")]
        public int StatusesCount { get; set; }

        /// <remarks/>
        [DataMember(Name="favourites_count")]
        public int FavouritesCount { get; set; }

        /// <remarks/>
        [DataMember(Name="created_at")]
        public string CreatedAt { get; set; }

        /// <remarks/>
        [DataMember(Name="geo_enabled")]
        public bool GeoEnabled { get; set; }

        /// <remarks/>
        [DataMember(Name="allow_all_act_msg")]
        public bool AllowAllActMsg { get; set; }

        /// <remarks/>
        [DataMember(Name="following")]
        public bool Following { get; set; }

        /// <remarks/>
        [DataMember(Name="verified")]
        public bool Verified { get; set; }

        /// <remarks/>
        [DataMember(Name="status")]
        public StatusInfo LatestStatus { get; set; }

    }
}
