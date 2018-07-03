using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class NonImFix : Annuities
    {
        public NonImFix(Person person) : base(person)
        {
            NonQualified();
            Immediate();
            CalculateData();
            Fixed();
        }
    }
}
