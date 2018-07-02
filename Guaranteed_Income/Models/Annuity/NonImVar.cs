using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class NonImVar : Annuities
    {
        public NonImVar(Person person) : base(person)
        {
            NonQualified();
            Immediate();
            CalculateData();
            Variable();
        }
    }
}
