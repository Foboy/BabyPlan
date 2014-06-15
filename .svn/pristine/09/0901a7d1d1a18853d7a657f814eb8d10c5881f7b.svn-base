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
    /// Represents a group of counters.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Counters
    {
        private Collection<CounterInfo> items = new Collection<CounterInfo>();

        /// <summary>
        /// Gets the counter.
        /// </summary>
        [DataMember(Name="count")]
        public Collection<CounterInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
