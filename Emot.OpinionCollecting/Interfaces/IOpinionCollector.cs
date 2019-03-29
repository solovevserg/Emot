using Emot.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting.Interfaces
{
    public interface IOpinionCollector
    {
        Task<IEnumerable<Opinion>> GetAsync(); //Get<TParser, TResult, TUri>(TUri uri) where TUri: IUri where TParser : IParser<, TUri>;
    }
}
