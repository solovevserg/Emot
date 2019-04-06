using Emot.Common.Models;
using Emot.OpinionCollecting.Collectors.Citilink.Parsers;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting.Collectors.Citilink
{
    public class CitilinkOpinionCollector : IOpinionCollector
    {
        private readonly int categoriesFrom;
        private readonly int categoriesCount;
        private readonly int goodsPageCount;

        public CitilinkOpinionCollector(int categoriesFrom = 0, int categoriesCount = int.MaxValue, int categoryPagesCount = int.MaxValue)
        {
            this.categoriesFrom = categoriesFrom;
            this.categoriesCount = categoriesCount;
            this.goodsPageCount = categoryPagesCount;
        }

        public async Task<IEnumerable<Opinion>> GetAsync()
        {
            var loader= new WebLoader();
            var mainPage = new CitilinkMainPageUri();
            var firstPageUris = await loader.LoadAsync(mainPage, new CitilinkMainPageParser());
            var goodsPageUris = await loader.LoadFlattenManyAsync(firstPageUris.Skip(categoriesFrom).Take(categoriesCount), new CitilinkCatergoryFirstPageParser(goodsPageCount));
            var opinionsPageUris = await loader.LoadFlattenManyAsync(goodsPageUris, new CitilinkCategoryPageParser());
            var opinions = await loader.LoadFlattenManyAsync(opinionsPageUris, new CitilinkOpinionsPageParser());
            return opinions;
        }
    }
}
