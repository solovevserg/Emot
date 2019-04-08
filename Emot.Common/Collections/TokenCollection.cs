using Emot.Common.Models;
using Emot.Common.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Emot.Common.Collections
{
    public class TokenCollection : Dictionary<string, OccurencesClassCollection>
    {
        //private readonly List<Token> _tokens = new List<Token>();

        public TokenCollection()
        {
        }

        public TokenCollection(IDictionary<string, OccurencesClassCollection> dictionary) : base(dictionary)
        {
        }

        public TokenCollection(IEnumerable<KeyValuePair<string, OccurencesClassCollection>> collection) : base(collection)
        {
        }

        public void AddOccurence(OpinionClass @class, string occurence)
        {
            EnsureContainsKey(occurence); 
            this[occurence].AddOccurences(@class);
        }

        public void AddOccurences(OpinionClass @class, IEnumerable<string> occurences)
        {
            foreach (var occurence in occurences)
            {
                AddOccurence(@class, occurence);
            }
        }

        public void AddOccurences(OpinionClass @class, string occurence, int count)
        {
            EnsureContainsKey(occurence);
            this[occurence].AddOccurences(@class, count);
        }

        private void EnsureContainsKey(string occurence)
        {
            if (!ContainsKey(occurence))
            {
                this[occurence] = new OccurencesClassCollection();
            }
        }

        public static TokenCollection FromIEnumerable(IEnumerable<Token> tokens)
        {
            var collection = new TokenCollection();
            foreach (var token in tokens)
            {
                foreach (var tokenOccurence in token.Occurences)
                {
                    collection.AddOccurences(tokenOccurence.OpinionClass, token.TokenText, tokenOccurence.Count);
                }
            }
            return collection;
        }
    }
}
