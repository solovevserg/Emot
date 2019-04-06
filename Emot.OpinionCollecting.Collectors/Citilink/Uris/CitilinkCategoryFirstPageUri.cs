using Emot.OpinionCollecting.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting.Collectors.Citilink.Uris
{
    class CitilinkCategoryFirstPageUri : Uri
    {
        public CitilinkCategoryFirstPageUri(string uri) : base(uri + "?sorting=opinions_desc")
        {
        }

        //private readonly string _categoryPath;

        //public CitilinkCategoryFirstPageUri(string categryPath)
        //{
        //    _categoryPath = categryPath;
        //}

        //public string Get() => $"https://www.citilink.ru/catalog/{_categoryPath}";
    }
}
