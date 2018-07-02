using Guaranteed_Income.Models;
using Guaranteed_Income.Utilities;
using System;

namespace Guaranteed_Income.Interfaces
{
    public abstract class Annuities
    {
        public double minPremium { get; set; } = 56918; //the average minimum premium amoung insurance companies
        public double rate { get; set; } = 0.0311275; //the average return rate amoung insurance companies
        public double initialAmount { get; set; }
        public double extraPayments { get; set; }
        public double accumulationYears { get; set; }
        public double growthPercentage { get; set; }
        public double lumpSumAtRetirement { get; set; }
        public double extraFees { get; set; }
        public double addedRider { get; set; }
        public double yearsAfterRetirement { get; set; }
        public double distributionsBeforeTax { get; set; }
        public double exclusionRatio { get; set; }
        public double irsLife { get; set; }
        public double annuityLife { get; set; }
        public double retireLife { get; set; }
        public const int defferedTime = 5;
        public double effectiveRate { get; set; }
        public double totalExpectedReturn { get; set; }
        public double yearlyTaxable { get; set; }
        public double yearlyNonTaxable { get; set; }
        public double yearsOfPayments { get; set; }

        public Annuities(Person person)
        {
            int currentYear = DateTime.Now.Year;
            int yearsToRetirement = Int32.Parse(person.retirementDate) - currentYear;

            irsLife = LifeExpectancy.GetLifeExpectancy(person.age, person.gender);
            annuityLife = LifeExpectancy.GetLifeExpectancy(person.age + defferedTime, person.gender);
            retireLife = LifeExpectancy.GetLifeExpectancy(person.age + yearsToRetirement, person.gender);
            initialAmount = person.lumpSum;
            accumulationYears = Math.Max(yearsToRetirement, defferedTime);
            effectiveRate = person.income / (person.taxBracket.stateYearlyTax + person.taxBracket.federalYearlyTax);
            yearsAfterRetirement = (Int32.Parse(person.deathDate) - Int32.Parse(person.retirementDate));
        }

        public void CalculateData()
        {
            distributionsBeforeTax = new PaymentCalculator(lumpSumAtRetirement, rate, yearsOfPayments).GetPayments();
            totalExpectedReturn = distributionsBeforeTax * yearsOfPayments;
           
            yearlyTaxable = (1 - exclusionRatio) * distributionsBeforeTax;
            yearlyNonTaxable = distributionsBeforeTax = yearlyTaxable;
        }

        public void NonQualified()
        {
            initialAmount = initialAmount * (1 - effectiveRate);
            exclusionRatio = initialAmount / totalExpectedReturn;
        }

        public void Qualified()
        {
            exclusionRatio = 0;
        }

        public void Deferred()
        {
            lumpSumAtRetirement = new FutureValue(initialAmount, rate, accumulationYears).GetFutureValue();
        }

        public void Immediate()
        {
            yearsOfPayments = (int)irsLife;
            lumpSumAtRetirement = initialAmount;
        }

        public void Fixed()
        {

        }

        public void Variable()
        {

        }
    }
}
