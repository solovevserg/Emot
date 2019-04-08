using Emot.Common.Collections;
using Emot.Common.Models.Enums;
using Emot.Stemming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emot.SentimentAnalysis.UnigramAnalyser
{
    public class UnigramNaiveAnalyser
    {
        private readonly TokenCollection _tokens;

        public UnigramNaiveAnalyser(TokenCollection tokens)
        {
            _tokens = tokens;
        }

        public async Task<(double, double)> AnalyseAsync(string text)
        {
            var stemmer = new Stemmer();
            var words = await stemmer.StemAsync(text);
            int pos = 0;
            int neg = 0;
            double assessment = 1;
            int count = 0;

            foreach (var word in words)
            {
                if(!_tokens.ContainsKey(word))
                {
                    continue;
                }
                //if(_tokens[word].Count != 2)
                //{
                //    continue;
                //}

                if (_tokens[word].ContainsKey(OpinionClass.Positive))
                {
                    pos += _tokens[word][OpinionClass.Positive];
                }
                if (_tokens[word].ContainsKey(OpinionClass.Negative))
                {
                    neg += _tokens[word][OpinionClass.Negative];
                }
                count++;
            }
            assessment *= (double)pos / neg;
            Console.WriteLine($"{count} words were used for estimation of text ({words.Count()} words)");
            return (pos, neg);
        }
    }
}
