using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Emot.Common.Collections
{
    public class OccurencesClassCollection : Dictionary<OpinionClass, int>
    {
        public OccurencesClassCollection()
        {
        }

        public OccurencesClassCollection(IDictionary<OpinionClass, int> dictionary) : base(dictionary)
        {
        }

        public OccurencesClassCollection(IEnumerable<KeyValuePair<OpinionClass, int>> collection) : base(collection)
        {
        }

        public void AddOccurences(OpinionClass @class, int count = 1) {
            if(!ContainsKey(@class))
            {
                this[@class] = 0;
            }
            this[@class] += count;
        }
    }
}
