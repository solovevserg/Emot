using Emot.Common.Interfaces;
using Emot.Common.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emot.Common.Models
{
    public class Token //: IReadOnlyToken
    {
        public int Id { get; set; }

        [Required]
        public string TokenText { get; set; }

        [Required]
        public TokenType TokenType { get; set; }

        public List<TokenOccurence> Occurences { get; set; } = new List<TokenOccurence>();

        // In Purposes Of Caching
        public double Frequency { get; set; }

        //[NotMapped]
        //IEnumerable<IReadOnlyTokenOccurence> IReadOnlyToken.Occurences => Occurences;
    }
}
