using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class NonImVar : Annuities
    {
        public NonImVar(Person person, Brokerage stock) : base(person)
        {
            NonQualified();
            Immediate();
            Variable();
            CalculateData();
            yearlyBreakdown = GetYearlyBreakdown(stock);
            afterTaxIncome = distributionsBeforeTax * (1 - taxRate) + yearlyNonTaxable;
            afterTaxIncome = Math.Round(afterTaxIncome, 2);
            assetValue = Math.Max(person.assetIncome * (1 - taxRate), 0);
            Math.Round(assetValue, 2);
        }
    }
}
