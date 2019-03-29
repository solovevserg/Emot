using AngleSharp;
using AngleSharp.Dom;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting
{
    public class WebLoader
    {
        private readonly IConfiguration _config = Configuration.Default.WithDefaultLoader();

        public async Task<TResult> LoadAsync<TParser, TResult>(IUri uri) where TParser: IParser<TResult>, new()
        {
            var address = uri.Get();
            var document = await OpenAsync(address);
            var parser = new TParser();
            TResult result = parser.Parse(document);
            return result;
        }

        private async Task<IDocument> OpenAsync(string address)
        {
            var context = BrowsingContext.New(_config);
            var document = await context.OpenAsync(address);
            return document;
        }
    }
}
