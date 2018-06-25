using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Guaranteed_Income.Models
{
    public class InputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
