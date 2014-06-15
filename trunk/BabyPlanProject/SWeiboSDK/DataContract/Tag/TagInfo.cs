using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a group of tag.
    /// </summary>
    [Serializable]
    [DataContract]
    public class TagInfo
    {
        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        [DataMember(Name = "id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        [DataMember(Name = "value")]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the tag value.
        /// </summary>
        [DataMember(Name="weight")]
        public int Weight { get; set; }

    }
}
