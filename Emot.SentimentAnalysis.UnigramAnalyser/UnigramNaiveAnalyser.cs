using Emot.Common.Models;
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
        private readonly List<Token> _tokens;

        public UnigramNaiveAnalyser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public async Task<(double, double)> AnalyseAsync(string text)
        {
            var stemmer = new Stemmer();
            var words = await stemmer.StemAsync(text);
            double pos = 0;
            double neg = 0;
            double assessment = 1;
            int count = 0;

            foreach (var word in words)
            {
                var token = _tokens.Where(t => t.Occurences.Count >= 2).FirstOrDefault(t => t.TokenText == word);
                if(token == null)
                {
                    continue;
                }
                pos += token.Occurences.FirstOrDefault(o => o.OpinionClass == OpinionClass.Positive)?.Count ?? 0;
                neg += token.Occurences.FirstOrDefault(o => o.OpinionClass == OpinionClass.Negative)?.Count ?? 0;
                
                count++;
            }
            assessment *= (double)pos / neg;
            Console.WriteLine($"{count} words were used for estimation of text ({words.Count()} words)");
            return (pos, neg);
        }
    }
}
