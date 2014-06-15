using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a tag id.
    /// </summary>
    [Serializable]
    [DataContract]
    public class TagID
    {
        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [DataMember(Name="tagid")]
        public long ID
        {
            get;
            set;
        }
    }
}
