using Emot.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.Database.Repositories
{
    public interface IOpinionRepository
    {
        void AddOpinion(Opinion opinion);

        void AddOpinions(IEnumerable<Opinion> opinions);
    }
}
