using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting
{
    public class Uri : IUri
    {
        protected readonly string _uri;

        public Uri(string uri)
        {
            _uri = uri;
        }

        public string Get() => _uri;
    }
}
