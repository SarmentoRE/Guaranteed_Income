using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Annuity
{
    public class QualImFix : Annuities
    {
        public QualImFix(Person person, Brokerage stock) : base(person)
        {
            Qualified();
            Immediate();
            Fixed();
            CalculateData();
            yearlyBreakdown = GetYearlyBreakdown(stock);
            afterTaxIncome = distributionsBeforeTax * (1 - taxRate);
            afterTaxIncome = Math.Round(afterTaxIncome, 2);
            assetValue = Math.Max(person.assetIncome * (1 - taxRate), 0);
            Math.Round(assetValue, 2);
        }
    }
}
