using AngleSharp;
using AngleSharp.Dom;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emot.OpinionCollecting
{
    public class WebLoader
    {
        private readonly IConfiguration _config = Configuration.Default.WithDefaultLoader();

        public async Task<TResult> LoadAsync<TResult, TUri>(TUri uri, IParser<TResult, TUri> parser) where TUri : IUri
        {
            var address = uri.Get();
            Console.WriteLine("Starting connection to " + address);
            var document = await OpenAsync(address);
            TResult result = parser.Parse(document);
            Console.WriteLine("Loaded and parsed: " + result);
            return result;
        }

        public async Task<IEnumerable<TResult>> LoadManyAsync<TResult, TUri>(IEnumerable<TUri> uris, IParser<TResult, TUri> parser) where TUri : IUri
        {
            var list = new List<TResult>();
            foreach (var uri in uris)
            {
                var result = await LoadAsync(uri, parser);
                list.Add(result);
            }
            return list;
        }

        public async Task<IEnumerable<TResult>> LoadFlattenManyAsync<TResult, TUri>(IEnumerable<TUri> uris, IParser<IEnumerable<TResult>, TUri> parser) where TUri : IUri 
        {
            var results = await LoadManyAsync(uris, parser);
            return results.SelectMany(elem => elem);
        }

        private async Task<IDocument> OpenAsync(string address)
        {
            var context = BrowsingContext.New(_config);
            var document = await context.OpenAsync(address);
            return document;
        }
    }
}
