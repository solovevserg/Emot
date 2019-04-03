using Emot.Common.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emot.Database.Repositories
{
    public interface  IUnigramRepository
    {
        Task AddTokenCollection(TokenCollection tokens);
    }
}
