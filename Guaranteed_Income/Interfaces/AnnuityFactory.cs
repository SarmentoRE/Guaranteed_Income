using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Utilities;
using System;

namespace Guaranteed_Income.Models
{
    public class AnnuityFactory : Annuities
    {
        public AnnuityFactory(AnnuityTax tax, AnnuityTime time, AnnuityRate rate, Person person, Brokerage stock) : base(person)
        {
            annuityTax = tax;
            annuityTime = time;
            annuityRate = rate;

            //Set up annuity details based on tax type
            if (tax == AnnuityTax.Nonqualified) NonQualified();
            else if (tax == AnnuityTax.Qualified) Qualified();

            //Set up annuity details based on time
            if (time == AnnuityTime.Deferred) Deferred();
            else if (time == AnnuityTime.Immediate) Immediate();

            //Set up annuity details based on rate
            if (rate == AnnuityRate.Fixed) Fixed();
            else if (rate == AnnuityRate.Variable) Variable();

            CalculateBrokerageData(stock);
        }

        public void NonQualified()
        {
            initialAmount = initialAmount * (1 - effectiveRate);
        }

        public void Qualified()
        {
            //nothing special currently happens for qualified, but this is here just in case it changes in the future
        }

        public void Deferred()
        {
            yearsOfPayments = Math.Min((int)annuityLife, (int)retireLife);
            lumpSumAtRetirement = FutureValue.GetFutureValue(initialAmount, rate, accumulationYears);
        }

        public void Immediate()
        {
            yearsOfPayments = (int)irsLife;
            lumpSumAtRetirement = initialAmount;
        }

        public void Fixed()
        {
            extraFees = 0;
        }

        public void Variable()
        {
            extraFees = 0.025;
        }
    }

    public enum AnnuityTax
    {
        Nonqualified,
        Qualified
    }
    public enum AnnuityTime
    {
        Immediate,
        Deferred
    }
    public enum AnnuityRate
    {
        Variable,
        Fixed
    }
}
