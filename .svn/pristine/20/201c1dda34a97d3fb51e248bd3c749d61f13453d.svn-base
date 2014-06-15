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
    /// Represents a set of status.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Statuses 
    {
        private Collection<StatusInfo> items;

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [DataMember(Name="statuses")]
        public Collection<StatusInfo> Items
        {
            get
            {
                if(null == items)
                    items = new Collection<StatusInfo>();
                return items;
            }
        }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        [DataMember(Name = "total_number")]
        public int TotalCount { get; set; }
        
        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        [DataMember(Name = "previous_cursor")]
        public long PreviousCursor { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [DataMember(Name = "next_cursor")]
        public long NextCursor { get; set; }
        
        /// <summary>
        /// Unknown.
        /// </summary>
        [DataMember(Name = "hasvisible")]
        public bool HasVisible { get; set; }
    }
}
