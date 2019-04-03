using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting
{
    class Uri : IUri
    {
        private readonly string _uri;

        public Uri(string uri)
        {
            _uri = uri;
        }
        public string Get() => _uri;
    }
}
