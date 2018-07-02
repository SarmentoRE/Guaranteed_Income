using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class QualDefFix : Annuities
    {   
        public QualDefFix(Person person) : base(person)
        {
            Qualified();
            Deferred();
            CalculateData();
            Fixed();
        }
    }
}
