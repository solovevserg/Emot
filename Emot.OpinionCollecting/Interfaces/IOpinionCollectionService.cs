using Emot.Common.Models;
using System.Collections.Generic;

namespace Emot.OpinionCollecting.Interfaces
{
    public interface IOpinionCollectionService
    {
        IEnumerable<Opinion> Collect(IOpinionCollector collector);
    }
}
