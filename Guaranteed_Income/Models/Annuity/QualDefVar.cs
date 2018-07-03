using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class QualDefVar : Annuities
    {
        public QualDefVar(Person person) : base(person)
        {
            Qualified();
            Deferred();
            Variable();
            CalculateData();
        }
    }
}
