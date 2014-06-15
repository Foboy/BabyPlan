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
    /// Represents a group of trends(topic).
    /// </summary>
    [Serializable]
    [DataContract]
    public class PeriodTrends
    {
        /// <summary>
        /// Gets or sets the time of the trend.
        /// </summary>
        [DataMember(Name = "trends")]
        public PeriodTrendsItems Trends { get; set; }

        /// <summary>
        /// Gets or sets the as-of of the trend.
        /// </summary>
        [DataMember(Name = "as_of")]
        public long? AsOf { get; set; }

    }

    /// <summary>
    /// Represents the items of period trends.
    /// </summary>
    [DataContract]
    public class PeriodTrendsItems
    {
        private Collection<PeriodTrendInfo> items;

        /// <summary>
        /// Gets the trends.
        /// </summary>
        [DataMember(Name = "trend")]
        public Collection<PeriodTrendInfo> Items
        {
            get
            {
                if (null == items)
                    items = new Collection<PeriodTrendInfo>();

                return items;
            }
        }

        /// <summary>
        /// Gets or sets the time of the trend.
        /// </summary>
        [DataMember(Name = "time")]
        public string Time { get; set; }
    }
}
