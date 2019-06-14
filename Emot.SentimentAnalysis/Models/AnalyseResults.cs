using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.SentimentAnalysis.Models
{
    class AnalyseResults : Dictionary<OpinionClass, double>
    {
        public int WordsCount { get; set; }

        public AnalyseResults()
        {
            
        }
    }
}
