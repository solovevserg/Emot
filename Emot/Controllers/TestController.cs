using Emot.Common.Collections;
using Emot.Database.Context;
using Emot.Database.Repositories;
using Emot.MockData;
using Emot.SentimentAnalysis.UnigramAnalyser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emot.Controllers
{
    public class TestController : Controller
    {
        private readonly IOpinionRepository _opinionsRepository;
        private readonly IUnigramRepository _tokenRepository;
        private readonly EmotDbContext _context;

        public TestController(
            IOpinionRepository opinionsRepository,
            IUnigramRepository tokenRepository,
            EmotDbContext context
            )
        {
            _opinionsRepository = opinionsRepository;
            _tokenRepository = tokenRepository;
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            //_context.RemoveRange(_context.Opinions);
            //_context.RemoveRange(_context.Tokens);
            //_context.SaveChanges();

            //var collector = new CitilinkOpinionCollector(0, 10, 3);
            //var opinions = await collector.GetAsync();
            //Console.WriteLine($"Opinions colledcted {opinions.Count()}");

            //_opinionsRepository.AddOpinions(opinions);
            //Console.WriteLine($"Opinions stored in Db");

            //var stemmer = new Stemmer();
            //Console.WriteLine($"Stemming is starting");

            //var opinionsCount = _context.Opinions.Count();
            //for (int i = 0; i < opinionsCount; i += 50)
            //{
            //    Task.Run(async () =>
            //    {
            //        var opinions = _context.Opinions.Skip(i).Take(50);
            //        var tokenCollection = await stemmer.StemAsync(opinions);
            //        Console.WriteLine($"Done! Token collection {tokenCollection.Count}");
            //        await _tokenRepository.AddTokenCollection(tokenCollection);
            //        Console.WriteLine($"Done! Token collection was stored in Db");
            //    }).Wait();
            //    Console.WriteLine($"{i} opinions processed.");
            //}

            //var tokens = _context.Tokens.Include(t => t.Occurences).ToList();
            //var positiveOccurences = _context.TokenOccurences.Where(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Positive);
            //var posCount = positiveOccurences.Sum(o => o.Count);
            //foreach (var occurence in positiveOccurences)
            //{
            //    occurence.Frequency = (double)occurence.Count / posCount;
            //}
            //var negOccurences = _context.TokenOccurences.Where(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Negative);
            //var negCount= negOccurences.Sum(o => o.Count);
            //foreach (var occurence in negOccurences)
            //{
            //    occurence.Frequency = (double)occurence.Count / negCount;
            //}

            //var posCount = _context.Tokens.Sum(t => t.Occurences.First(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Positive).Count);
            //var negCount = _context.Tokens.Sum(t => t.Occurences.First(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Negative).Count);

            _context.SaveChanges();

            return Json(_context.Tokens);

        }

        public IActionResult CalculateFrequencies()
        {
            var tokens = _context.Tokens.Include(t => t.Occurences);

            var count = tokens.Sum(t => t.Occurences.Sum(o => o.Count));

            foreach (var token in tokens)
            {
                token.Frequency = (double)token.Occurences.Sum(o => o.Count) / count;
            }
            _context.SaveChanges();

            return View("Tokens", _context.Tokens.Include(t => t.Occurences));
        }

        public IActionResult Opinions()
        {
            return View(_context.Opinions);
        }

        public IActionResult Tokens()
        {
            var tokens = _context.Tokens.Include(t => t.Occurences).ToList();//.Where(t => t.Occurences.Count >= 2).ToList();
            var sortedTokens = tokens.OrderByDescending(t =>
            {
                var positive = t.Occurences.FirstOrDefault(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Positive);
                var negative = t.Occurences.FirstOrDefault(o => o.OpinionClass == Common.Models.Enums.OpinionClass.Negative);
                if (positive == null || negative == null)
                {
                    return 0;
                }
                return (double)positive.Count / negative.Count;
            });
            return View(sortedTokens);
        }

        public async Task<IActionResult> Putin()
        {
            string text = PutinSpeach.Text;
            var paragraphs = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ViewBag.Text = text;
            ViewBag.Paragraphs = paragraphs.Select(p => p.Split(' ')).ToList();

            var tokens = (_context.Tokens.Include(t => t.Occurences)).ToList();
            ViewBag.Tokens = TokenCollection.FromIEnumerable(tokens);
            var analyser = new UnigramNaiveAnalyser(tokens);
            var results = new List<(double, double)>();
            foreach (var p in paragraphs)
            {
                Console.WriteLine("Next paragraph");
                results.Add(await analyser.AnalyseAsync(p));
            }
            ViewBag.Results = results;
            return View();
        }
    }
}