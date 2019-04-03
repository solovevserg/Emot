using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Uris
{
    class CitilinkCategoryFirstPageUri : IUri
    {
        private readonly string _categoryPath;

        public CitilinkCategoryFirstPageUri(string categryPath)
        {
            _categoryPath = categryPath;
        }

        public string Get() => $"https://www.citilink.ru/catalog/{_categoryPath}";
    }
}
