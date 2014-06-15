using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents the API rate limit status.
    /// </summary>
    [Serializable]
    [DataContract]
    public class RateLimitStatus
    {
        /*
         <?xml version="1.0" encoding="UTF-8"?><hash><remaining-hits type="integer">1000</remaining-hits><hourly-limit type="integer">1000</hourly-limit><reset-time-in-seconds type="integer">1539</reset-time-in-seconds><reset-time type="datetime">Sat Aug 27 10:00:00 +0800 2011</reset-time></hash>
    "ip_limit": 10000,
    "limit_time_unit": "HOURS",
    "remaining_ip_hits": 10000,
    "remaining_user_hits": 150,
    "reset_time": "2011-06-03 18:00:00",
    "reset_time_in_seconds": 1415,
    "user_limit": 150,
         */

        private Collection<APIRateLimit> apiRateLimits;
        
        /// <remarks/>
        [DataMember(Name = "api_rate_limits")]
        public Collection<APIRateLimit> APIRateLimits
        {
            get
            {
                if (null == apiRateLimits)
                    apiRateLimits = new Collection<APIRateLimit>();

                return apiRateLimits;
            }
        }

        /// <remarks/>
        [DataMember(Name = "ip_limit")]
        public int IPLimit { get; set; }

        /// <remarks/>
        [DataMember(Name = "limit_time_unit")]
        public string LimitTimeUnit { get; set; }

        /// <remarks/>
        [DataMember(Name = "remaining_user_hits")]
        public int RemainingUserHits { get; set; }

        /// <remarks/>
        [DataMember(Name = "reset_time")]
        public string ResetTime { get; set; }

        /// <remarks/>
        [DataMember(Name = "reset_time_in_seconds")]
        public long ResetTimeInSeconds { get; set; }

        /// <remarks/>
        [DataMember(Name = "user_limit")]
        public int UserLimit { get; set; }
    }

    /// <summary>
    /// Represents APIRateLimit.
    /// </summary>
    [DataContract]
    public class APIRateLimit
    {
        /// <remarks/>
        [DataMember(Name = "api")]
        public string API { get; set; }

        /// <remarks/>
        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        /// <remarks/>
        [DataMember(Name = "limit_time_unit")]
        public string LimitTimeUnit { get; set; }

        /// <remarks/>
        [DataMember(Name = "remaining_hits")]
        public int RemainingHits { get; set; }

    }
}
