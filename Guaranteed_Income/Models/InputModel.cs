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
        public JObject personOne { get; set; }

        public JObject personTwo { get; set; }
    }
}
