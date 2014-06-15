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
    /// Represents a group of users.
    /// </summary>
    [Serializable]
    [DataContract(Name = "users")]
    public class Users 
    {
        private Collection<UserInfo> items;

        /// <summary>
        /// Gets the users.
        /// </summary>
        [DataMember(Name="users")]
        public Collection<UserInfo> Items
        {
            get
            {
                // This is required, otherwise Json Deserialization fails.
                if (null == items)
                    items = new Collection<UserInfo>();

                return items;
            }
        }

        /// <summary>
        /// Gets or sets the next page cursor.
        /// </summary>
        [DataMember(Name = "next_cursor")]
        public int NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous page cursor.
        /// </summary>
        [DataMember(Name = "previous_cursor")]
        public int PreviousCursor { get; set; }
        
        /// <summary>
        /// Gets or sets the total_number.
        /// </summary>
        [DataMember(Name = "total_number")]
        public int TotalNumber { get; set; }        
    }
}
