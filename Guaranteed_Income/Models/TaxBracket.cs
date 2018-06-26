using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class TaxBracket
    {
        private double federalYearlyTax;
        private double stateYearlyTax;
        private List<double> rate;
        private List<double> bracket;

        public TaxBracket(int income, FilingStatus status)
        {
            switch (status) //add any filing status rates here
            {
                case FilingStatus.Joint :
                    this.rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    this.bracket = new List<double> { 0, 19050, 77400, 165000, 315000, 400000, 600000 };
                    break;
                case FilingStatus.Single :
                    this.rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    this.bracket = new List<double> { 0, 9525, 38700, 82500, 157500, 200000, 500000 };
                    break;
                case FilingStatus.HeadOfHousehold:
                    this.rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    this.bracket = new List<double> { 0, 13600, 51800, 82500, 157500, 200000, 500000 };
                    break;
                case FilingStatus.Married:
                    this.rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    this.bracket = new List<double> { 0, 9525, 38700, 82500, 157500, 200000, 300000 };
                    break;
            }
            this.federalYearlyTax = CalculateYearlyFederalTax(income);
            this.stateYearlyTax = CalculateYearlyStateTax(income);
        }

        public double CalculateYearlyFederalTax(int income)
        {
            double tax = 0;
            double currentAmount = (Double)income;
            int currentBracket = 0;
            double taxable;
            //Find the maximum applicable tax bracket
            bracket.ForEach(element =>
            {
                if (currentAmount > element)
                {
                    currentBracket = bracket.IndexOf(element);
                }
            });

            for (int i = currentBracket; i >= 0; i--) //TAX THEM
            {
                taxable = currentAmount - bracket[i];
                tax += taxable * rate[i];
                currentAmount = currentAmount - taxable;
            }
            return tax;
        }

        public double CalculateYearlyStateTax(int income)
        {
            this.rate = new List<double> { 0.02, .03, .05, .0575 };
            this.bracket = new List<double> { 0, 3000, 5000, 17000 };
            return CalculateYearlyFederalTax(income);
        }
    }

    public enum FilingStatus //currently supported filing statuses
    {
        Single,
        Joint,
        HeadOfHousehold,
        Married
    }
}
