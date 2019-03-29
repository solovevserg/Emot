using AngleSharp.Dom;
using Emot.Common.Models;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    public class CitilinkOpinionsPageParser : IParser<IEnumerable<Opinion>>
    {
        public IEnumerable<Opinion> Parse(IDocument document)
        {
            var articles = document.QuerySelectorAll("#opinionBox .opinion article").Where(article => article.ChildElementCount == 6);
            var positiveOpinions = articles.Select(article => article.Children.ElementAt(1)).Select(ul => ul.TextContent);
            return positiveOpinions.Select(opinion => new Opinion()
            {
                Text = opinion,
                Scrapped = DateTime.Now,
                OpinionClass = Common.Models.Enums.OpinionClass.Positive,
                Source = document.Url
            });
        }
    }
}
