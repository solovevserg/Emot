using Emot.Database.Context;
using Emot.Database.Repositories;
using Emot.OpinionCollecting.Collectors.Citilink;
using Emot.Stemming;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            //_context.RemoveRange(_context.Tokens);
            //_context.RemoveRange(_context.Opinions);
            //_context.SaveChanges();

            var collector = new CitilinkOpinionCollector(0, 10, 3);
            var opinions = await collector.GetAsync();
            Console.WriteLine($"Opinions colledcted {opinions.Count()}");

            _opinionsRepository.AddOpinions(opinions);
            Console.WriteLine($"Opinions stored in Db");

            var stemmer = new Stemmer();
            Console.WriteLine($"Stemming is starting");

            var tokenCollection = await stemmer.StemAsync(opinions);
            Console.WriteLine($"Done! Token collection {tokenCollection.Count}");

            await _tokenRepository.AddTokenCollection(tokenCollection);
            Console.WriteLine($"Done! Token collection was stored in Db");

            var tokens = _context.Tokens.Include(t => t.Occurences);

            foreach (var token in tokens)
            {
                Console.Write($"Token: {token.TokenText}");
                foreach (var classOccurence in token.Occurences)
                {
                    Console.Write($" {classOccurence.OpinionClass} - {classOccurence.Count}");
                }
                Console.WriteLine();
            }
            return Json(tokens);

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
            return View(tokens);
        }
    }
}