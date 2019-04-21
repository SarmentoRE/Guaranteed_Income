using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Riders
{
    public class GMWBRider : IRider
    {
        public double rollUp { get; } = 0.06;
        public double minimumPercentage { get; } = 0.04;
        public double fee { get; } = 0.00525;
        private int accumulation = 5;

        public GMWBRider(Person person, Annuities annuity)
        {
            int growthYears = Math.Max(accumulation, person.retirementDate - DateTime.Now.Year);

            if (annuity.annuityTime == AnnuityTime.Immediate)
                growthYears = 0;

            benifitBase = FutureValue.GetFutureValue(annuity.initialAmount, rollUp, growthYears);

            annualIncome = (benifitBase * minimumPercentage) * (1 - fee);
            annuity.distributionsBeforeTax = annualIncome;
            annuity.gmwb = true;

            annuity.yearsOfPayments = (int) Math.Round(benifitBase / annualIncome);

            if (annuity.annuityTax == AnnuityTax.Qualified)
                annuity.exclusionRatio = annuity.initialAmount / benifitBase;
            else
                annuity.exclusionRatio = 0;
        }
    }
}
