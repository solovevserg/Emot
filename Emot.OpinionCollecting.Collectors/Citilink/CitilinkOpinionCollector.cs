using Emot.Common.Models;
using Emot.OpinionCollecting.Collectors.Citilink.Parsers;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting.Collectors.Citilink
{
    public class CitilinkOpinionCollector : IOpinionCollector
    {
        public async Task<IEnumerable<Opinion>> GetAsync()
        {
            var loader= new WebLoader();
            var mainPage = new CitilinkMainPageUri();
            //var a = await loader.LoadAsync<CitilinkMainPageParser, >(mainPage);

            var uri = new CitilinkOpinionsPageUri("/mobile/cell_phones", "1008932");
            var opinions = await loader.LoadAsync<CitilinkOpinionsPageParser, IEnumerable<Opinion>>(uri);
            return opinions;
        }


    }
}
