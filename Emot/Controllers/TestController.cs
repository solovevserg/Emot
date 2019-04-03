using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emot.Database.Context;
using Emot.Database.Repositories;
using Emot.OpinionCollecting.Collectors.Citilink;
using Emot.Stemming;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var collector = new CitilinkOpinionCollector();
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

            foreach (var opinion in _context.Opinions)
            {
                Console.WriteLine(opinion.Scrapped.ToShortDateString() + opinion.Text);
            }

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
            return Json(new object());

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
            return View(_context.Tokens.Include(t => t.Occurences));
        }
    }
}