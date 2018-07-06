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
        public double rollUpRate { get; } = 0.06;
        public double minimumPercentage { get; } = 0.04;
        public double benifitBase { get; set; }
        public double fee { get; } = 0.00525;
        //public double annualIncome { get; set; }
        private int accumulation = 5;

        public GMWBRider(Person person, Annuities annuity)
        {
            int growthYears = Math.Max(accumulation, person.retirementDate - DateTime.Now.Year);
            if (annuity.imm) growthYears = 0;
            FutureValue future = new FutureValue(annuity.initialAmount, minimumPercentage, growthYears);
            benifitBase = future.GetFutureValue();

            annualIncome = benifitBase * minimumPercentage;
            annualIncome = annualIncome * (1 - fee);
            annuity.distributionsBeforeTax = annualIncome;

            annuity.yearsOfPayments = (int)(benifitBase / annualIncome);

            if (annuity.qual)
            {
                annuity.exclusionRatio = annuity.initialAmount / benifitBase;
            }
            else if (annuity.qual == false)
            {
                annuity.exclusionRatio = 0;
            }
        }
    }
}
