using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a counter info of a status.
    /// </summary>
    [Serializable]
    [DataContract]
    public class CounterInfo
    {
        /// <remarks/>
        [DataMember(Name = "id")]
        public long StatusID { get; set; }

        /// <remarks/>
        [DataMember(Name = "comments")]
        public int Comments { get; set; }

        /// <remarks/>
        [DataMember(Name = "rt")]
        public int Forwards { get; set; }
    }
}
