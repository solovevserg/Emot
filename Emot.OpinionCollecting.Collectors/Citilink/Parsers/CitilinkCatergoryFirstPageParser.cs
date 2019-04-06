using AngleSharp.Dom;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    class CitilinkCatergoryFirstPageParser : IParser<IEnumerable<CitilinkCategoryPageUri>, CitilinkCategoryFirstPageUri>
    {
        private readonly int _pageCount;

        public CitilinkCatergoryFirstPageParser(int pageCount = int.MaxValue)
        {
            _pageCount = pageCount;
        }
        public IEnumerable<CitilinkCategoryPageUri> Parse(IDocument document)
        {
            var listingUris = document.QuerySelectorAll(".page_listing li a").Select(a => a.GetAttribute("href")).ToList();
            var submenuItemsUris = document.QuerySelectorAll(".subnavigation catalog-content-navigation__item li a").Select(a => a.GetAttribute("href"));
            listingUris.AddRange(submenuItemsUris);
            listingUris.Add(document.BaseUri);
            Console.WriteLine($"There are {listingUris.Count} links on this page");
            return listingUris.Take(_pageCount).Select(uri => new CitilinkCategoryPageUri(uri));
        }
    }
}
