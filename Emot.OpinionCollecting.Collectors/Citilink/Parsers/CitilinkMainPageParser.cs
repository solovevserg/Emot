using AngleSharp.Dom;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    class CitilinkMainPageParser : IParser<IEnumerable<CitilinkCategoryFirstPageUri>>
    {
        public IEnumerable<CitilinkCategoryFirstPageUri> Parse(IDocument document)
        {
            var categories = document.QuerySelectorAll("menu.menu_categories a.subcategory-list-item__link.h3").Select(e => e.GetAttribute("href"));
            var categoriesPathes = categories.Select(c => c.Split("catalog/").Last());
            var categoriesUris = categoriesPathes.Select(c => new CitilinkCategoryFirstPageUri(c));
            return categoriesUris;
        }
    }
}