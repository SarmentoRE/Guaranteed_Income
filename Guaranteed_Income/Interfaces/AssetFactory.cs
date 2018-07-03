using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class AssetFactory
    {
        public double yearlyIncome { get; set; }
        private const double rate = 0.03;

        public AssetFactory(AssetModel assets, string retirementDate, string deathDate, double income)
        {
            int currentYear = DateTime.Now.Year;
            int yearsToRetirement = Int32.Parse(retirementDate) - currentYear;
            int yearsOfRetirement = Int32.Parse(deathDate) - Int32.Parse(retirementDate);

            double additions = double.Parse(assets.additionsHolder[0]);
            double currentValue = double.Parse(assets.amountHolder[0]);

            additions += Math.Min((double.Parse(assets.matchHolder[0]) * additions), (double.Parse(assets.capHolder[0])*income*double.Parse(assets.matchHolder[0])));

            for (int i = yearsToRetirement; i > 0; i--)
            {
                currentValue += additions;
                currentValue += (currentValue * rate);
            }
            yearlyIncome = Math.Round((currentValue / (double)yearsOfRetirement),2);
        }
    }

    public enum AssetType
    {
        Unknown,
        _401k,
        R401k,
        IRA,
        RIRA
    }
}