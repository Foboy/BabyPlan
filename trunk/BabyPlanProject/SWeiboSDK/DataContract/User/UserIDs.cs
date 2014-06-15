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
    /// Represents the user ids.
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserIDs
    {
        private Collection<long> ids;

        /// <summary>
        /// Gets the statuses.
        /// </summary>        
        [DataMember(Name="ids")]
        public Collection<long> IDs
        {
            get
            {
                if(null == ids)
                    ids = new Collection<long>();
                return ids;
            }
        }

        /// <remarks/>
        [DataMember(Name = "next_cursor")]
        public int NextCursor { get; set; }

        /// <remarks/>
        [DataMember(Name = "previous_cursor")]
        public int PreviousCursor { get; set; }

        /// <summary>
        /// Gets or sets the total_number.
        /// </summary>
        [DataMember(Name = "total_number")]
        public int TotalNumber { get; set; }    
    }
}
