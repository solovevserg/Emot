using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Uris
{
    class CitilinkCategoryPageUri : IUri
    {
        private readonly string _categoryPath;
        private readonly string _pageNumber;

        public CitilinkCategoryPageUri(string categoryPath, string pageNumber)
        {
            _categoryPath = categoryPath;
            _pageNumber = pageNumber;
        }

        public string Get() => $"https://www.citilink.ru/catalog/{_categoryPath}/{_pageNumber}/otzyvy";

    }
}
