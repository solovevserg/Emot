using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emot.Common.Models
{
    class TokenClassOccurence
    {
        public int Id { get; set; }

        [Required]
        public int TokenId { get; set; }

        public Token Token { get; set; }

        [Required]
        public OpinionClass OpinionClass { get; set; }

        [Required]
        public int Count { get; set; }

        // In Purposes Of Caching
        public double Frequency { get; set; }
    }
}
