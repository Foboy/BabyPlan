using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a trend(topic).
    /// </summary>
    [Serializable]
    [DataContract]
    public class TrendInfo
    {
        /// <summary>
        /// Gets or sets the trend id.
        /// </summary>
        [DataMember(Name="trend_id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the hotword of the trend.
        /// </summary>
        [DataMember(Name = "hotword")]
        public string HotWord { get; set; }

        /// <summary>
        /// Gets or sets the number of hits the trend has.
        /// </summary>
        [DataMember(Name = "num")]
        public long Hits { get; set; }

    }

    /// <summary>
    /// Represents a trend(topic).
    /// </summary>
    [Serializable]
    [DataContract(Name="trend")]
    public class PeriodTrendInfo
    {
        /// <summary>
        /// Gets or sets the name of the trend.
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the query of the trend.
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the amount of the trend.
        /// </summary>
        [DataMember(Name = "amount")]
        public int Mmount { get; set; }

        /// <summary>
        /// Gets or sets the delta of the trend.
        /// </summary>
        [DataMember(Name = "delta")]
        public int Delta { get; set; }

    }
}
