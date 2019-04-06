using AngleSharp.Dom;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    class CitilinkCategoryPageParser : IParser<IEnumerable<CitilinkOpinionsPageUri>, CitilinkCategoryPageUri>
    {
        public IEnumerable<CitilinkOpinionsPageUri> Parse(IDocument document)
        {
            var uris = document.QuerySelectorAll(".subcategory-product-item__body .opinions a").Select(a => a.GetAttribute("href"));
            Console.WriteLine($"There are {uris.Count()} goods at this page");
            return uris.Select(uri => new CitilinkOpinionsPageUri(uri));
        }
    }
}
