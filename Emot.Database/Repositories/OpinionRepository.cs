using Emot.Common.Models;
using Emot.Database.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emot.Database.Repositories
{
    internal class OpinionRepository : IOpinionRepository
    {
        private readonly EmotDbContext _context;

        public OpinionRepository(EmotDbContext context)
        {
            _context = context;
        }

        public void AddOpinion(Opinion opinion)
        {
            _context.Add(opinion);
            _context.SaveChanges();
        }

        public void AddOpinions(IEnumerable<Opinion> opinions)
        {
            _context.AddRange(opinions);
            _context.SaveChanges();
        }
    }
}