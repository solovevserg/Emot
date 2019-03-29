using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.Common.Interfaces
{
    public interface IReadOnlyToken
    {
        string TokenText { get; }

        double Frequency { get; }

        IEnumerable<IReadOnlyTokenOccurence> Occurences { get; }
    }
}
