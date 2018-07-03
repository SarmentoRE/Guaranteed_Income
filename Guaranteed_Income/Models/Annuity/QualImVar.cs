using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class QualImVar : Annuities
    {
        public QualImVar(Person person) : base(person)
        {
            Qualified();
            Immediate();
            CalculateData();
            Variable();
        }
    }
}
