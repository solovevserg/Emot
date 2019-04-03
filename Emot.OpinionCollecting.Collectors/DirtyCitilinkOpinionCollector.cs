using Emot.Common.Models;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting.Collectors
{
    class DirtyCitilinkOpinionCollector : IOpinionCollector
    {
        private readonly WebLoader _loader = new WebLoader();

        public Task<IEnumerable<Opinion>> GetAsync()
        {
            throw new NotImplementedException();
        }


    }
}
