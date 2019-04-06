using AngleSharp.Dom;

namespace Emot.OpinionCollecting.Interfaces
{
    public interface IParser<TResult, TUri> where TUri : IUri
    {
        TResult Parse(IDocument document);
    }
}
