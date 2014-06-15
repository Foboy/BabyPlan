﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a geo point.
    /// </summary>
    [Serializable]
    [DataContract]
    public class GeoPoint
    {
        /// <summary>
        /// Gets or sets the geo content (in string form)
        /// </summary>
        /// <example>125.12 253.62</example>
        [XmlText]
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        [XmlIgnore]
        public string Lat
        {
            get
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    var groups = Content.Split(' ');
                    if (1 < groups.Length)
                        return groups[0];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        [XmlIgnore]
        public string Long
        {
            get
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    var groups = Content.Split(' ');
                    if(1 < groups.Length)
                        return groups[1];
                }

                return string.Empty;
            }
        }
    }
}
