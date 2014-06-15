using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AMicroblogAPI.DataContract
{
    /// <summary>
    /// Represents a group of emotions.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Emotions
    {
        private Collection<EmotionInfo> emotions;

        /// <summary>
        /// Gets the emotions.
        /// </summary>
        [DataMember(Name="emotions")]
        public Collection<EmotionInfo> Items
        {
            get
            {
                if(null == emotions)
                    emotions = new Collection<EmotionInfo>();
                return emotions;
            }
        }
    }
}
