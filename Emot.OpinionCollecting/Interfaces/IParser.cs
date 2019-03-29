using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.OpinionCollecting.Interfaces
{
    public interface IParser<TResult>
    {
        TResult Parse(IDocument document);
    }
}
