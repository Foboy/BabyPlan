using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Collections;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a group of trends(topic).
    /// </summary>
    [Serializable]
    [DataContract()]
    public class Trends
    {
        private Collection<TrendInfo> items;

        // Canot set both XmlArray and XmlElement attributes on here.
        /// <summary>
        /// Gets the trends.
        /// </summary>
        [DataMember(Name="userTrend")] // If XmlElement is set on a ICollection member, the member xml tag is ignored and each item uses the xml tag of the element name specified by the XmlElement.
        public Collection<TrendInfo> Items
        {
            get
            {
                if(null == items)
                    items = new Collection<TrendInfo>();
                return items;
            }
        }

        ///// <summary>
        ///// Gets or sets the time of the trend.
        ///// </summary>
        //[DataMember(Name = "time")]
        //public string Time { get; set; }

        ///// <summary>
        ///// Gets or sets the as-of of the trend.
        ///// </summary>
        //[DataMember(Name = "as_of")]
        //public long? AsOf { get; set; }

    }
}
