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
        public List<string> assetHolder;

        [Required]
        public List<string> amountHolder;

        [Required]
        public List<string> additionsHolder;

        [Required]
        public List<string> matchHolder;

        [Required]
        public List<string> capHolder;
    }
}
