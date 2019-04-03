using AngleSharp.Dom;
using Emot.Common.Models;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emot.OpinionCollecting.Collectors.Citilink.Parsers
{
    public class CitilinkOpinionsPageParser : IParser<IEnumerable<Opinion>>
    {
        public IEnumerable<Opinion> Parse(IDocument document)
        {
            var articles = document.QuerySelectorAll("#opinionBox .opinion article").Where(article => article.ChildElementCount == 6);
            var positiveUlElements = articles.Select(article => article.Children.ElementAt(1)).Select(ul => ul.TextContent);
            var positiveOpinions = positiveUlElements.Select(opinion => new Opinion()
            {
                Text = opinion,
                Scrapped = DateTime.Now,
                OpinionClass = Common.Models.Enums.OpinionClass.Positive,
                Source = document.Url
            });
            var negativeUlElements = articles.Select(article => article.Children.ElementAt(3)).Select(ul => ul.TextContent);
            var negativeOpinions = negativeUlElements.Select(opinion => new Opinion()
            {
                Text = opinion,
                Scrapped = DateTime.Now,
                OpinionClass = Common.Models.Enums.OpinionClass.Negative,
                Source = document.Url
            });
            return positiveOpinions.Concat(negativeOpinions);
        }
    }
}
