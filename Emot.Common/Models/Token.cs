using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emot.Common.Models
{
    class Token
    {
        public int Id { get; set; }

        [Required]
        public string TokenText { get; set; }

        [Required]
        public TokenType TokenType{ get; set; }
        
        public List<TokenClassOccurence> Occurences { get; set; }

        // In Purposes Of Caching
        public double Frequency { get; set; }
    }
}
