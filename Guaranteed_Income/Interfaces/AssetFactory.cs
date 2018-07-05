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

            try
            {
                double additions = double.Parse(assets.additions[0]);
                double currentValue = double.Parse(assets.amounts[0]);

                additions += Math.Min(((double.Parse(assets.matching[0])/100) * additions), ((double.Parse(assets.caps[0])/100) * income * (double.Parse(assets.matching[0])/100)));

                for (int i = yearsToRetirement; i > 0; i--)
                {
                    currentValue += (currentValue * rate);
                    currentValue += additions;
                }

                PaymentCalculator calculator = new PaymentCalculator(currentValue, rate, yearsOfRetirement);
                yearlyIncome = calculator.GetPayments();
            }
            catch(ArgumentOutOfRangeException e)
            {
                yearlyIncome = 0;
            }
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