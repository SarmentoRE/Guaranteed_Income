using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Models;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Riders
{
    public class GLWBRider : IRider
    {
        public double rollUp { get; } = 0.06;
        public double benifitBase { get; set; }
        public double lifetimeWithdrawlRate { get; set; }
        //public double annualIncome { get; set; }
        public double fee { get; } = 0.0075;
        private int accumulation = 5;

        public GLWBRider(Person person, Annuities annuity)
        {
            annuity.glwb = true;
            int growthYears = Math.Max(accumulation, person.retirementDate - DateTime.Now.Year);
            if (annuity.imm) growthYears = 0; 
            FutureValue future = new FutureValue(annuity.initialAmount, rollUp , growthYears);
            benifitBase = future.GetFutureValue();
            lifetimeWithdrawlRate = (LifeExpectancy.glwbTable[person.age+growthYears]);

            annualIncome = lifetimeWithdrawlRate * benifitBase;
            annualIncome = annualIncome * (1 - fee);
            annuity.distributionsBeforeTax = annualIncome;

            if (annuity.qual)
            {
                annuity.exclusionRatio = annuity.initialAmount / benifitBase;
            }
            else if(annuity.qual == false)
            {
                annuity.exclusionRatio = 0;
            }
        }
    }
}