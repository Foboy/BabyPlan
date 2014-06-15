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
    /// Represents an error response from remote server.
    /// <remarks>
    /// {"error":"User does not exists!","error_code":20003,"request":"/2/users/show.json"}
    /// </remarks>
    /// </summary>
    [Serializable]
    [XmlRoot("hash")]
    [DataContract]
    [DebuggerDisplay("Error:{ErrorCode}:{ErrorMessage}")]
    public class ErrorResponse
    {
        /// <remarks/>
        [XmlElement("request")]
        [DataMember(Name = "request")]
        public string Uri { get; set; }

        /// <remarks/>
        [XmlElement("error_code")]
        [DataMember(Name = "error_code")]
        public int ErrorCode { get; set; }

        /// <remarks/>
        [XmlElement("error")]
        [DataMember(Name = "error")]
        public string ErrorMessage { get; set; }
    }
}
