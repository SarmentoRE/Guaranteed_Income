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
        private double federalDeduction;
        private double stateDeduction;
        private List<double> rate;
        private List<double> bracket;

        public TaxBracket(int income, FilingStatus status, State state)
        {
            CalculateStandardDeduction(status, state);

            double federalTaxableIncome = (Double)Math.Max((income - federalDeduction),0);
            double stateTaxableIncome = (Double)Math.Max((income - stateDeduction),0);

            federalYearlyTax = CalculateYearlyFederalTax(federalTaxableIncome);
            stateYearlyTax = CalculateYearlyStateTax(stateTaxableIncome);
        }

        private double CalculateYearlyFederalTax(double income)
        {
            double tax = 0;
            double currentAmount = income;
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

        private double CalculateYearlyStateTax(double income)
        {
            rate = new List<double> { 0.02, .03, .05, .0575 };
            bracket = new List<double> { 0, 3000, 5000, 17000 };
            return CalculateYearlyFederalTax(income);
        }

        private void CalculateStandardDeduction(FilingStatus status, State state)
        {
            switch (status) //add any filing status rates here
            {
                case FilingStatus.Joint:
                    rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    bracket = new List<double> { 0, 19050, 77400, 165000, 315000, 400000, 600000 };
                    federalDeduction = 12000;
                    switch (state)
                    {
                        case State.Virginia:
                            stateDeduction = 6000;
                            break;
                    }
                    break;
                case FilingStatus.Single:
                    rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    bracket = new List<double> { 0, 9525, 38700, 82500, 157500, 200000, 500000 };
                    federalDeduction = 24000;
                    switch (state)
                    {
                        case State.Virginia:
                            stateDeduction = 3000;
                            break;
                    }
                    break;
                case FilingStatus.HeadOfHousehold:
                    rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    bracket = new List<double> { 0, 13600, 51800, 82500, 157500, 200000, 500000 };
                    federalDeduction = 18000;
                    switch (state)
                    {
                        case State.Virginia:
                            stateDeduction = 3000;
                            break;
                    }
                    break;
                case FilingStatus.Married:
                    rate = new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 };
                    bracket = new List<double> { 0, 9525, 38700, 82500, 157500, 200000, 300000 };
                    federalDeduction = 12000;
                    switch (state)
                    {
                        case State.Virginia:
                            stateDeduction = 3000;
                            break;
                    }
                    break;
            }
        }
    }

    public enum FilingStatus //currently supported filing statuses
    {
        Single,
        Joint,
        HeadOfHousehold,
        Married
    }

    public enum State
    {
        Virginia
    }
}
