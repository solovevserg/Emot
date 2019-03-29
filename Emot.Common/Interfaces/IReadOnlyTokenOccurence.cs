using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.Common.Interfaces
{
    public interface IReadOnlyTokenOccurence
    {
        OpinionClass OpinionClass { get; }

        int Count { get; }

        double Frequency { get; }
    }
}
