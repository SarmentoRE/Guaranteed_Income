using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Guaranteed_Income.Models
{
    public class InputModel
    {
        [Required]
        public String gender { get; set; }

        [Required]
        public List<String> concerns { get; set; }

        [Required]
        public int income { get; set; }

        [Required]
        public int additions { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        public String filingStatus { get; set; }

        [Required]
        public String retirementDate { get; set; }

        [Required]
        public String deathDate { get; set; }
    }
}