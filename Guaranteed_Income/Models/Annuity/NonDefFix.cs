using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class NonDefFix : Annuities
    {
        public NonDefFix(Person person, Brokerage stock) : base(person)
        {
            NonQualified();
            Deferred();            
            Fixed();
            CalculateData();
        }
    }
}
