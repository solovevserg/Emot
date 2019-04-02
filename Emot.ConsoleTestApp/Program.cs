using Emot.Common.Collections;
using Emot.Common.Models.Enums;
using Emot.OpinionCollecting.Collectors.Citilink;
using System;
using System.Threading.Tasks;

namespace Emot.ConsoleTestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task = Task.Run(async () =>
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

                var collector = new CitilinkOpinionCollector();
                var opinions = await collector.GetAsync();
                foreach (var opinion in opinions)
                {
                    Console.WriteLine(opinion.Text);
                }
            });
            task.Wait();
            Console.ReadKey();
        }
    }
}
