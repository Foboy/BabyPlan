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
    /// Represents a set of comment.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Comments 
    {
        private Collection<CommentInfo> items = new Collection<CommentInfo>();

        /// <summary>
        /// Gets the comments.
        /// </summary>
        [DataMember(Name="comments")]
        public Collection<CommentInfo> Items
        {
            get
            {
                if(null == items)
                    items = new Collection<CommentInfo>();
                return items;
            }
        }

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
