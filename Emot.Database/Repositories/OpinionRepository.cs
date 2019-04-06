using Emot.Common.Models;
using Emot.Database.Context;
using System.Collections.Generic;
using System.Linq;
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
            AddOneOpinion(opinion);
            _context.SaveChanges();
        }

        private void AddOneOpinion(Opinion opinion)
        {
            bool isDuplicate = _context.Opinions.Any(o => o.Source == opinion.Source);
            if (!isDuplicate)
            {
                _context.Add(opinion);
            }
        }

        public void AddOpinions(IEnumerable<Opinion> opinions)
        {
            foreach (var opinion in opinions)
            {
                AddOneOpinion(opinion);
            }
            _context.SaveChanges();
        }
    }
}