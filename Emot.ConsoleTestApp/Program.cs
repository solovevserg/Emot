using Emot.Common.Collections;
using Emot.Common.Models;
using Emot.Common.Models.Enums;
using Emot.OpinionCollecting.Collectors.Citilink;
using Emot.Stemming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emot.ConsoleTestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task = Task.Run(async () =>
            {
                var collector = new CitilinkOpinionCollector();
                var opinions = await collector.GetAsync();
                Console.WriteLine($"Opinions colledcted {opinions.Count()}");
                var stemmer = new Stemmer();
                Console.WriteLine($"Stemming is starting");
                var tokenCollection = await stemmer.StemAsync(opinions);
                Console.WriteLine($"Done! Token collection {tokenCollection.Count}");
                //OutputTokenCollection(tokenCollection);
            });
            task.Wait();
            Console.ReadKey();
        }

        private static void OutputTokenCollection(TokenCollection tokenCollection)
        {
            foreach (var token in tokenCollection)
            {
                Console.WriteLine(token.Key);
                foreach (var @class in token.Value)
                {
                    Console.WriteLine("   - " + @class.Key.ToString() + " " + @class.Value);
                }
            }
        }
    }
}
