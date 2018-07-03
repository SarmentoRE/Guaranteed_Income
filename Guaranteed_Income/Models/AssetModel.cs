using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class AssetModel
    {
        [Required]
        public List<string> assets;

        [Required]
        public List<string> amounts;

        [Required]
        public List<string> additions;

        [Required]
        public List<string> matching;

        [Required]
        public List<string> caps;
    }
}
