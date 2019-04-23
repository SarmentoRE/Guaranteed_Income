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
        public List<List<double>> yearlyBreakdown { get; set; } = new List<List<double>>();
        public double taxRate { get; set; }
        private Person person { get; set; }
        public IRider rider { get; set; }
        public double assetValue { get; set; }
        public AnnuityRate annuityRate { get; set; }
        public AnnuityTax annuityTax { get; set; }
        public AnnuityTime annuityTime { get; set; }
        public bool glwb;
        public bool DB;
        public bool gmwb;

        public Annuities(Person person)
        {
            int currentYear = DateTime.Now.Year;
            yearsToRetirement = person.retirementDate - currentYear;

            //Calculate life expectancy based on different real world methods
            irsLife = LifeExpectancy.GetLifeExpectancy(person.age, person.gender);
            annuityLife = LifeExpectancy.GetLifeExpectancy(person.age + defferedTime, person.gender);
            retireLife = LifeExpectancy.GetLifeExpectancy(person.age + yearsToRetirement, person.gender);

            initialAmount = person.lumpSum;
            accumulationYears = Math.Max(yearsToRetirement, defferedTime);
            effectiveRate = Tax.GetEffectiveRate(person);
            yearsAfterRetirement = (person.deathDate - person.retirementDate);
            this.person = person;

            CalculateRiders();
        }

        public void CalculateBrokerageData(Brokerage stock)
        {
            rate -= extraFees;

            distributionsBeforeTax = PaymentCalculator.GetPayments(lumpSumAtRetirement, rate, yearsOfPayments);
            totalExpectedReturn = distributionsBeforeTax * yearsOfPayments;

            //exclusion ratio is only non-zero if it a nonqualified annuity   
            if (annuityTax == AnnuityTax.Nonqualified)
                exclusionRatio = initialAmount / totalExpectedReturn;
            else
                exclusionRatio = 0;

            CalculateRiders();
            CalculateTaxes();

            yearlyBreakdown = GetYearlyBreakdown(stock);
            if(annuityTax == AnnuityTax.Nonqualified)
                afterTaxIncome = distributionsBeforeTax * (1 - taxRate) + yearlyNonTaxable;
            else
                afterTaxIncome = distributionsBeforeTax * (1 - taxRate);

            afterTaxIncome = Math.Round(afterTaxIncome, 2);
            assetValue = Math.Max(person.assetIncome * (1 - taxRate), 0);
            Math.Round(assetValue, 2);
        }

        public void CalculateTaxes()
        {
            double totalYearlyTax;
            double taxable;
            yearlyTaxable = (1 - exclusionRatio) * distributionsBeforeTax;
            yearlyNonTaxable = distributionsBeforeTax - yearlyTaxable;
            if (person.assetType == AssetType.RIRA || person.assetType == AssetType.R401k)
            {
                taxable = yearlyTaxable;
                yearlyNonTaxable += person.assetIncome;
            }
            else
            {
                taxable = yearlyTaxable + person.assetIncome;
            }

            totalYearlyTax =  Tax.GetSpecialTax(taxable, person.filingStatus, person.state, (person.age + yearsToRetirement));

            taxRate = totalYearlyTax / yearlyTaxable;
            afterTaxIncome = (taxable * (1-taxRate)) + yearlyNonTaxable;
            afterTaxIncome = Math.Round(afterTaxIncome, 2);
        }

        public void CalculateRiders()
        {
            if (person.concerns[0])
            {

            }
            if (person.concerns[1])
            {
                GLWBRider rider = new GLWBRider(person, this);
                this.rider = rider;
            }
            if (person.concerns[2])
            {
                GMWBRider rider = new GMWBRider(person, this);
                this.rider = rider;
            }
            if (person.concerns[3])
            {
                DBRider rider = new DBRider(person, this);
                this.rider = rider;
            }
        }

        public List<List<double>> GetYearlyBreakdown(Brokerage stock)
        {
            List<List<double>> yearlyBreakdown = new List<List<double>>();
            List<double> currentYear = new List<double>();

            double currentAmount = 80000;
            double carloRate;

            if (DB)
            {
                yearsOfPayments -= 5;
            }
            if (glwb)
            {
                yearsOfPayments += 5;
            }
            stock.yearsOfLife = yearsOfPayments;
            if (annuityRate == AnnuityRate.Variable)
            {
                carloRate = ((rate * 0.6) + (stock.growth75 * 0.4));
                yearlyBreakdown.Add(VariableCalc(carloRate));
                CalculateTaxes();
                carloRate = ((rate * 0.6) + (stock.growth90 * 0.4));
                yearlyBreakdown.Add(VariableCalc(carloRate));

            }
            else
            {
                currentYear.Add(currentAmount);
                if (annuityTime == AnnuityTime.Deferred)
                {
                    for (int i = 0; i < yearsToRetirement; i++)
                    {
                        currentAmount += (lumpSumAtRetirement - 80000) / yearsToRetirement;
                        currentYear.Add(currentAmount);
                    }
                }
                for (int i = 0; i < yearsOfPayments; i++)
                {
                    currentAmount += currentAmount * rate;

                    currentAmount -= distributionsBeforeTax;

                    currentYear.Add(currentAmount);
                }
                yearlyBreakdown.Add(currentYear);
            }
            if (DB)
            {
                leftOverMoney = totalExpectedReturn - (distributionsBeforeTax * yearsOfPayments);
            }

            return yearlyBreakdown;
        }

        //calculates how much money a variable annuity will payout for a given year
        private List<double> VariableCalc(double carloRate)
        {
            List<double> currentYear = new List<double>();
            double currentAmount = initialAmount;

            lumpSumAtRetirement = FutureValue.GetFutureValue(initialAmount, carloRate, yearsToRetirement);
            currentYear.Add(initialAmount);
            if (annuityTime == AnnuityTime.Deferred)
            {
                for (int j = 0; j < yearsToRetirement; j++)
                {
                    currentAmount += lumpSumAtRetirement / yearsToRetirement;
                    currentYear.Add(currentAmount);
                }
            }
            distributionsBeforeTax = PaymentCalculator.GetPayments(currentAmount, carloRate, yearsOfPayments);
            if (glwb) { yearsOfPayments += 100;}
            for (int j = 0; j < yearsOfPayments; j++)
            {
                currentAmount += currentAmount * carloRate;
                if (gmwb || glwb)
                {
                    currentAmount -= distributionsBeforeTax;
                    currentAmount = Math.Max((currentAmount - distributionsBeforeTax), (rider.annualIncome));
                   
                }
                else
                {
                    currentAmount -= distributionsBeforeTax;
                    currentAmount = Math.Max(0, currentAmount);
                }
                currentYear.Add(currentAmount);
            }
            return currentYear;
        }

        public static void FinishStock(Brokerage stock)
        {
            double currentAmmount75 = stock.amountAtRetirement75;
            double currentAmmount90 = stock.amountAtRetirement90;
            for (int i = 0; i < stock.yearsOfLife; i++)
            {
                currentAmmount75 += currentAmmount75 * stock.growth75;
                currentAmmount75 -= stock.withdrawl75;
                currentAmmount90 += currentAmmount90 * stock.growth90;
                currentAmmount90 -= stock.withdrawl90;

                currentAmmount75 = Math.Max(0, currentAmmount75);
                currentAmmount90 = Math.Max(0, currentAmmount90);
                currentAmmount75 = Math.Round(currentAmmount75, 2);
                currentAmmount90 = Math.Round(currentAmmount90, 2);
                stock.confident75.Add(currentAmmount75);
                stock.confident90.Add(currentAmmount90);
            }

        }
    }
}