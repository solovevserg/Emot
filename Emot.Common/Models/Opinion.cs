using Emot.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emot.Common.Models
{
    public class Opinion
    {
        public int Id { get; set; }

        [Required]
        public int Text { get; set; }

        [Required]
        public OpinionClass OpinionClass { get; set; }

        public string Source { get; set; }

        public DateTime Scrapped { get; set; }

        public string AuthorName { get; set; }

        public DateTime? Published{ get; set; }

        public OpinionType OpinionType { get; set; }
    }
}
