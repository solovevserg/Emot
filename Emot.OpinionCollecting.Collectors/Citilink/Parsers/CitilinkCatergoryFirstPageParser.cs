using AngleSharp.Dom;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    class CitilinkCatergoryFirstPageParser : IParser<IEnumerable<CitilinkCategoryPageUri>>
    {
        public IEnumerable<CitilinkCategoryPageUri> Parse(IDocument document)
        {
            throw new NotImplementedException();
            //var pageCountString = document.QuerySelector("#subcategoryList .page_listing ul li:last-child a").TextContent;
            //int pageCount = int.Parse(pageCountString);
            //var uris = Enumerable.Range(1, pageCount).Select(i => new CitilinkCategoryPageUri());
        }
    }
}
