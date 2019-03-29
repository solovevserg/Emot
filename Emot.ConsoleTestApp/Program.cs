using Emot.Common.Collections;
using Emot.Common.Models;
using Emot.Common.Models.Enums;
using Emot.OpinionCollecting;
using Emot.OpinionCollecting.Collectors.Citilink.Parsers;
using Emot.OpinionCollecting.Collectors.Citilink.Uris;
using Emot.OpinionCollecting.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emot.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var col = new TokenCollection();
            col.AddOccurence(OpinionClass.Positive, "отличный");
            col.AddOccurence(OpinionClass.Negative, "отличный");
            col.AddOccurence(OpinionClass.Positive, "отличный");
            col.AddOccurence(OpinionClass.Positive, "отличный");
            col.AddOccurence(OpinionClass.Positive, "отличный");
            col.AddOccurence(OpinionClass.Positive, "Здравый");
            col.AddOccurence(OpinionClass.Negative, "плохой");
            col.AddOccurence(OpinionClass.Negative, "плохой");
            col.AddOccurence(OpinionClass.Negative, "плохой");
            foreach (var token in col)
            {
                Console.WriteLine(token.Key);
                foreach (var @class in token.Value)
                {
                    Console.WriteLine("   - " + @class.Key.ToString() + @class.Value);
                }
            }

            var loader = new WebLoader();
            var task = Task.Run(async () =>
            {
                var opinions = await loader.LoadAsync<CitilinkOpinionsPageParser, IEnumerable<Opinion>>(new CitilinkOpinionsPageUri(null, null));
                foreach (var opinion in opinions)
                {
                    Console.WriteLine(opinion.Text);
                }

            });
            task.Wait();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
