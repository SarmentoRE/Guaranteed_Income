using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models.Riders
{
    public class DBRider : IRider
    {
        public double rollUp { get; } = 0.06;
        public double lifetimeWithdrawlRate { get; set; }
        public double fee { get; } = 0.0065;
        private int accumulation = 5;

        public DBRider(Person person, Annuities annuity)
        {
            annuity.DB = true;
            int growthYears = Math.Max(accumulation, person.retirementDate - DateTime.Now.Year);

            if (annuity.annuityTime == AnnuityTime.Immediate)
                growthYears = 0;

            annuity.rate -= (annuity.extraFees + fee);
            annuity.distributionsBeforeTax = PaymentCalculator.GetPayments(annuity.lumpSumAtRetirement, annuity.rate, annuity.yearsOfPayments);
            annuity.totalExpectedReturn = annuity.distributionsBeforeTax * annuity.yearsOfPayments;
            annuity.exclusionRatio = 0;

            if (annuity.annuityTax == AnnuityTax.Qualified)
                annuity.exclusionRatio = annuity.initialAmount / annuity.totalExpectedReturn;            
        }
    }
}
