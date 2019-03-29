using Emot.Common.Collections;
using Emot.Common.Models.Enums;
using System;
using System.Linq;

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
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
