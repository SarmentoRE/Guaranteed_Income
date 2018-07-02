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
        public string gender { get; set; }

        [Required]
        public List<String> concerns { get; set; }

        [Required]
        public double income { get; set; }

        [Required]
        public int additions { get; set; }

        [Required]
        public int lumpSum { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        public string filingStatus { get; set; }

        [Required]
        public string retirementDate { get; set; }

        [Required]
        public string deathDate { get; set; }

    }
}