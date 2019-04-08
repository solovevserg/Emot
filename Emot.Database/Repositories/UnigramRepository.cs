using Emot.Common.Collections;
using Emot.Common.Models;
using Emot.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emot.Database.Repositories
{
    class UnigramRepository : IUnigramRepository
    {
        private readonly EmotDbContext _context;

        public UnigramRepository(EmotDbContext context)
        {
            this._context = context;
        }

        public async Task AddTokenCollection(TokenCollection tokens)
        {
            foreach (var token in tokens.Keys)
            {
                await AddOccurences(token, tokens[token]);
            }
            _context.SaveChanges();
        }

        private async Task AddOccurences(string token, OccurencesClassCollection occurencesClassCollection)
        {
            var dbToken = await _context.Tokens.Include(t => t.Occurences).FirstOrDefaultAsync(t => t.TokenText == token);
            if(dbToken == null)
            {
                dbToken = new Token() { TokenText = token, TokenType = Common.Models.Enums.TokenType.Unigram };
                _context.Add(dbToken);
            }
            foreach (var @class in occurencesClassCollection.Keys)
            {
                var dbOccurence = dbToken.Occurences.FirstOrDefault(o => o.OpinionClass == @class);
                if(dbOccurence == null) {
                    dbOccurence = new TokenOccurence() { OpinionClass = @class };
                    dbToken.Occurences.Add(dbOccurence);
                }
                dbOccurence.Count += occurencesClassCollection[@class];
            }
        }
    }
}
