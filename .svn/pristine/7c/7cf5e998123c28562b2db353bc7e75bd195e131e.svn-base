using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents the relationship info of two users.
    /// </summary>
    [Serializable]
    [DataContract]
    public class RelationshipInfo
    {
        /// <remarks/>
        [DataMember(Name = "source")]
        public RelationshipPart Source { get; set; }

        /// <remarks/>
        [DataMember(Name = "target")]
        public RelationshipPart Target { get; set; }
    }
}
