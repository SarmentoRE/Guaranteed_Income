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
        //public double benifitBase { get; set; }
        public double lifetimeWithdrawlRate { get; set; }
        //public double annualIncome { get; set; }
        public double fee { get; } = 0.0075;
        private int accumulation = 5;
        private int growthYears;

        public GLWBRider(Person person, Annuities annuity)
        {          
            annuity.glwb = true;

            if (annuity.annuityTime == AnnuityTime.Immediate)
            {
                growthYears = 0;
                benifitBase = annuity.initialAmount;
            }
            else
            {
                growthYears = Math.Max(accumulation, person.retirementDate - DateTime.Now.Year);
                benifitBase = FutureValue.GetFutureValue(annuity.initialAmount, rollUp, growthYears);
            }

            //Limits should be imposed on the user to not allow glwb if they are out of the range annuities allow
            try
            {
                lifetimeWithdrawlRate = (LifeExpectancy.glwbTable[person.age + growthYears]);
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(exception);
                lifetimeWithdrawlRate = 0;
            }

            annualIncome = (lifetimeWithdrawlRate * benifitBase) * (1 - fee);
            annuity.distributionsBeforeTax = annualIncome;

            if (annuity.annuityTax == AnnuityTax.Qualified)
                annuity.exclusionRatio = annuity.initialAmount / benifitBase;
            else
                annuity.exclusionRatio = 0;
        }
    }
}