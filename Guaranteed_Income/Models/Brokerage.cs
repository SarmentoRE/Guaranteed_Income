using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class Brokerage
    { 
        private MonteCarlo carlo;

        public List<double> confident75 { get; set; }
        public List<double> confident90 { get; set; }
        public double withdrawl75 { get; set; }
        public double withdrawl90 { get; set; }
        public double amountAtRetirement75 { get; set; }
        public double amountAtRetirement90 { get; set; }
        public double growth75 { get; set; }
        public double growth90 { get; set; }
        public double? yearsOfLife { get; set; } = null;
        public double afterTaxIncome { get; set; }
        public double exclusionRatio { get; set; }
        public double yearlyTaxable { get; set; }
        public double distributionsBeforeTax { get; set; }
        public double yearlyNonTaxable { get; set; }
        private Person person;

        public Brokerage(MonteCarlo carlo, Person person)
        {
            this.carlo = carlo;
            this.person = person;
            GenerateOutput();
        }


        public void GenerateOutput()
        {
            Confidence confidence = new Confidence(carlo.trialsList);
            confident75 = confidence.FindInterval(75);
            confident90 = confidence.FindInterval(90);
            amountAtRetirement75 = confident75[confident75.Count - 1];
            amountAtRetirement90 = confident90[confident90.Count - 1];
            int yearsOfPayments = (person.deathDate - person.retirementDate);
            double currentAmmount75 = amountAtRetirement75;
            double currentAmmount90 = amountAtRetirement90;
            double totalAmount;

            if (yearsOfLife == null) yearsOfLife = yearsOfPayments;
      
            growth75 = ((amountAtRetirement75 / confident75[0])-1) / (person.retirementDate - DateTime.Now.Year);
            growth90 = ((amountAtRetirement90 / confident90[0])-1) / (person.retirementDate - DateTime.Now.Year);

            withdrawl75 = Math.Round(new PaymentCalculator(amountAtRetirement75, growth75, yearsOfPayments+1).GetPayments(),2);
            withdrawl90 = Math.Round(new PaymentCalculator(amountAtRetirement90, growth90, yearsOfPayments+1).GetPayments(),2);

            totalAmount = withdrawl75 * yearsOfPayments;
            exclusionRatio = person.lumpSum / totalAmount;
        }

        public void CalculateTaxes()
        {
            List<double> rate = new List<double>{ 0,.15,.20};
            List<double> bracket = new List<double>();
            switch (person.filingStatus)
            {
                case FilingStatus.Joint:
                     bracket = new List<double>{ 0, 77400, 480050 };
                    break;
                case FilingStatus.Married:
                    bracket = new List<double> { 0, 38700, 240025 };
                    break;
                case FilingStatus.HeadOfHousehold:
                    bracket = new List<double> { 0, 51850, 453350 };
                    break;
                case FilingStatus.Single:
                    bracket = new List<double> { 0, 38700, 426700 };
                    break;
            }
            double totalYearlyTax;
            double taxable;
            yearlyTaxable = (1 - exclusionRatio) * withdrawl75;
            yearlyNonTaxable = withdrawl75 - yearlyTaxable;
            if (person.assetType == AssetType.RIRA || person.assetType == AssetType.R401k)
            {
                taxable = yearlyTaxable;
                yearlyNonTaxable += person.assetIncome;
            }
            else
            {
                taxable = yearlyTaxable + person.assetIncome;
            }

            TaxBracket tax = new TaxBracket(taxable, person.filingStatus, person.state, (person.age + (DateTime.Now.Year - person.retirementDate)), rate, bracket, person.assetIncome);
            totalYearlyTax = tax.federalYearlyTax + tax.stateYearlyTax;

            afterTaxIncome = (taxable - totalYearlyTax) + yearlyNonTaxable;
            afterTaxIncome = Math.Round(afterTaxIncome, 2);
        }
    }
}
