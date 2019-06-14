using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.SentimentAnalysis.Abstractions
{
    public interface ISentimentAnalyser
    {
        Task<Dictionary<OpinionClass, double>> AnalyseAsync(string text);
    }
}
