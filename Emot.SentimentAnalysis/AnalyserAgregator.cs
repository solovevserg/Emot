using Emot.SentimentAnalysis.Abstractions;
using Emot.SentimentAnalysis.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emot.SentimentAnalysis
{
    public class AnalyserAgregator
    {
        private readonly IEnumerable<AnalyserInfo> _analysers;

        public AnalyserAgregator(IEnumerable<AnalyserInfo> analysers)
        {
            _analysers = analysers;
        }

        public AnalyseAsync(string text)
        {

        }
    }
    }
}
