using Guaranteed_Income.Models;
using Guaranteed_Income.Models.Riders;
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
        public double leftOverMoney { get; set; } = 0;
        private Person person { get; set; }
        private bool var;
        public bool imm;
        public bool qual;
        public bool glwb;
        public bool DB;

        public Annuities(Person person)
        {
            int currentYear = DateTime.Now.Year;
            yearsToRetirement = person.retirementDate - currentYear;

            irsLife = LifeExpectancy.GetLifeExpectancy(person.age, person.gender);
            annuityLife = LifeExpectancy.GetLifeExpectancy(person.age + defferedTime, person.gender);
            retireLife = LifeExpectancy.GetLifeExpectancy(person.age + yearsToRetirement, person.gender);
            initialAmount = person.lumpSum;
            accumulationYears = Math.Max(yearsToRetirement, defferedTime);
            effectiveRate = (person.taxBracket.stateYearlyTax + person.taxBracket.federalYearlyTax) / person.income;
            yearsAfterRetirement = (person.deathDate - person.retirementDate);
            this.person = person;

            CalculateRiders();
        }

        public void CalculateData()
        {

            rate -= extraFees;
 
            distributionsBeforeTax = new PaymentCalculator(lumpSumAtRetirement, rate, yearsOfPayments).GetPayments();
            totalExpectedReturn = distributionsBeforeTax * yearsOfPayments;
            exclusionRatio = 0;
            if (qual == false) exclusionRatio = initialAmount / totalExpectedReturn;
            CalculateRiders();
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
            imm = false;
        }

        public void Immediate()
        {
            yearsOfPayments = (int)irsLife;
            lumpSumAtRetirement = initialAmount;
            imm = true;
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
            afterTaxIncome = Math.Round(afterTaxIncome, 2);
        }

        public void CalculateRiders()
        {
            if (person.concerns[0])
            {

            }
            if(person.concerns[1])
            {
                GLWBRider rider = new GLWBRider(person, this);
            }
            if (person.concerns[2])
            {

            }
            if (person.concerns[3])
            {

            }
        }

        public List<List<double>> GetYearlyBreakdown(MonteCarlo carlo)
        {

            if (DB) {
               yearsOfPayments -= 5;
               leftOverMoney = totalExpectedReturn - (distributionsBeforeTax * yearsOfPayments);
            }
            if (glwb)
            {
                yearsOfPayments += 5;
            }

            if (var)
            {
                for(int i = 0; i < yearsOfPayments; i++)
                {

                }
            }
            else
            {

            }
        }
    }
}
