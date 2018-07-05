using Guaranteed_Income.Models;
using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;

namespace Guaranteed_Income.Interfaces
{
    public abstract class Annuities
    {
        public double minPremium { get; set; } = 56918; //the average minimum premium amoung insurance companies
        public double rate { get; set; } = 0.0311275; //the average return rate amoung insurance companies
        public double initialAmount { get; set; }
        public double extraPayments { get; set; }
        public double accumulationYears { get; set; }
        public double lumpSumAtRetirement { get; set; }
        public double extraFees { get; set; }
        public double addedRider { get; set; }
        public int yearsAfterRetirement { get; set; }
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
        public double afterTaxIncome { get; set; }
        public int yearsToRetirement { get; set; }
        private Person person { get; set; }
        private bool var;
        private bool qual;

        public Annuities(Person person)
        {
            int currentYear = DateTime.Now.Year;
            yearsToRetirement = Int32.Parse(person.retirementDate) - currentYear;

            irsLife = LifeExpectancy.GetLifeExpectancy(person.age, person.gender);
            annuityLife = LifeExpectancy.GetLifeExpectancy(person.age + defferedTime, person.gender);
            retireLife = LifeExpectancy.GetLifeExpectancy(person.age + yearsToRetirement, person.gender);
            initialAmount = person.lumpSum;
            accumulationYears = Math.Max(yearsToRetirement, defferedTime);
            effectiveRate = (person.taxBracket.stateYearlyTax + person.taxBracket.federalYearlyTax) / person.income;
            yearsAfterRetirement = (Int32.Parse(person.deathDate) - Int32.Parse(person.retirementDate));
            this.person = person;
        }

        public void CalculateData()
        {
            rate -= extraFees;

            distributionsBeforeTax = new PaymentCalculator(lumpSumAtRetirement, rate, yearsOfPayments).GetPayments();
            totalExpectedReturn = distributionsBeforeTax * yearsOfPayments;
            exclusionRatio = 0;
            if (qual == false) exclusionRatio = initialAmount / totalExpectedReturn;
            yearlyTaxable = (1 - exclusionRatio) * distributionsBeforeTax;
            yearlyNonTaxable = distributionsBeforeTax - yearlyTaxable;
            CalculateTaxes();
        }

        public void NonQualified()
        {
            initialAmount = initialAmount * (1 - effectiveRate);
            qual = false;
           
        }

        public void Qualified()
        {
            qual = true;
        }

        public void Deferred()
        {
            yearsOfPayments = Math.Min((int)annuityLife, (int)retireLife);
            lumpSumAtRetirement = new FutureValue(initialAmount, rate, accumulationYears).GetFutureValue();
        }

        public void Immediate()
        {
            yearsOfPayments = (int)irsLife;
            lumpSumAtRetirement = initialAmount;
        }

        public void Fixed()
        {
            extraFees = 0;
            var = false;

        }

        public void Variable()
        {
            extraFees = 0.025;
            var = true;
        }

        public void CalculateTaxes()
        {
            double totalYearlyTax;
            double taxable;
            if (person.assetType == AssetType.RIRA || person.assetType == AssetType.R401k)
            {
                taxable = yearlyTaxable;
                yearlyNonTaxable += person.assetIncome;
            }
            else
            {
                taxable = yearlyTaxable + person.assetIncome;
            }

            TaxBracket tax = new TaxBracket(taxable, person.filingStatus, person.state, (person.age + yearsToRetirement));
            totalYearlyTax = tax.federalYearlyTax + tax.stateYearlyTax;

            afterTaxIncome = (taxable - totalYearlyTax) + yearlyNonTaxable;
        }

        public double GetYearlyBreakdown(MonteCarlo carlo)
        {
            double currentAmount = initialAmount;
            List<List<double>> yearlyBreakdown = new List<List<double>>();
            Confidence confidence = new Confidence(carlo.trialsList);
            List<int> intervals = new List<int> { 25, 50, 75, 90 };
            List<double> currentYear = new List<double>();
            double carloRate;
            List<double> currentInterval = new List<double>();
            double retirementAmount;

            if (var == false)
            {
                for (int i = 0; i < yearsToRetirement; i++)
                {
                    currentAmount += lumpSumAtRetirement / yearsToRetirement;
                    currentYear.Add(currentAmount);
                }

                for (int i = 0; i < yearsOfPayments; i++)
                {
                    currentAmount -= distributionsBeforeTax;
                    currentYear.Add(currentAmount);
                }

                yearlyBreakdown.Add(currentYear);
            }
            else if (var)
            {
                for (int j = 0; j < intervals.Count; j++)
                {
                    currentInterval = confidence.FindInterval(intervals[j]);
                    carloRate = ((currentInterval[currentInterval.Count - 1] / currentInterval[0]) - 1) / currentInterval.Count;
                    currentAmount = initialAmount;

                    for (int i = 0; i < yearsToRetirement; i++)
                    {
                        currentAmount = currentAmount * (1 + (.6 * rate + .4 * carloRate));
                        currentYear.Add(currentAmount);
                    }

                    retirementAmount = new PaymentCalculator(currentAmount, (.6 * rate + .4 * carloRate), yearsOfPayments).GetPayments();

                    for (int i = 0; i < yearsOfPayments; i++)
                    {
                        currentAmount -= retirementAmount;
                        currentYear.Add(currentAmount);
                    }

                    yearlyBreakdown.Add(currentYear);
                }
            }
            return Math.Round(afterTaxIncome,2);
        }
    }
}
