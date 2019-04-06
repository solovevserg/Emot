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
        public IEnumerable<CitilinkCategoryPageUri> Parse(IDocument document)
        {
            var uris = document.QuerySelectorAll("#subcategoryList .page_listing li.last a").Select(a => a.GetAttribute("href")).ToList();
            uris.Add(document.BaseUri);
            return uris.Select(uri => new CitilinkCategoryPageUri(uri));
                
               
            
                
                //var pageCountString = document.QuerySelector("#subcategoryList .page_listing ul li:last-child a").TextContent;
            //int pageCount = int.Parse(pageCountString);
            //var uris = Enumerable.Range(1, pageCount).Select(i => new CitilinkCategoryPageUri());
        }
    }
}
