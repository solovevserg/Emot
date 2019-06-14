using Emot.SentimentAnalysis.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.SentimentAnalysis.Models
{
    public class AnalyserInfo
    {
        public ISentimentAnalyser Analyser { get; set; }

        public double Contribution { get; set; }
    }
}
