using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public static class Tax
    {
        //TODO: I'll eventually put all of this tax info into a config file
        private static List<double> FederalRate {get => new List<double> { 0.10, 0.12, 0.22, 0.24, 0.32, 0.35, 0.37 }; }
        private static List<double> FederalBracket { get => new List<double> { 0, 19050, 77400, 165000, 315000, 400000, 600000 }; }
        private static List<double> CapitolGainsRate { get => new List<double> { 0, .15, .20 }; }
        private static List<double> VirginiaRate { get => new List<double> { 0.02, .03, .05, .0575 }; }
        private static List<double> VirginiaBracket { get => new List<double> { 0, 3000, 5000, 17000 }; }

        public static double GetStateTax(Person person)
        {
            return TaxCalculator(GetStateRate(person.state), GetStateBracket(person.state), (person.income - GetStateStandardDeduction(person.filingStatus, person.state) ));
        }

        public static double GetTotalTax(Person person)
        {
            return (GetFederalTax(person) + GetStateTax(person));
        }

        public static double GetFederalTax(Person person)
        {
            return TaxCalculator(FederalRate, FederalBracket, (person.income - GetFederalStandardDeduction(person.filingStatus, person.age)));
        }

        public static double GetEffectiveRate(Person person)
        {
            return (GetStateTax(person) + GetFederalTax(person)) / person.income;
        }

        //allows tax calculations for custom income, status, state, or age that is not default in person
        public static double GetSpecialTax(double taxableIncome, FilingStatus status, State state, int age)
        {
            taxableIncome -= (GetStateStandardDeduction(status, state) + GetFederalStandardDeduction(status, age));

            return (TaxCalculator(FederalRate, FederalBracket, taxableIncome) + TaxCalculator(GetStateRate(state), GetStateBracket(state), taxableIncome) );
        }

        //only the increase in taxes only from capitol gains, regular income taxes are not included
        public static double GetCapitolGainsTax(double income, FilingStatus status, State state, int age, double capitolGains)
        {
            double federalTax = TaxCalculator(CapitolGainsRate, GetCapitolGainsBracket(status), capitolGains);
            double stateTax = 0;
            double stateDeduction = GetStateStandardDeduction(status, state);
            List<double> stateRate;
            List<double> stateBracket;

            switch (state)
            {
                //capitol gains are taxed as normal income in VA
                case State.Virginia:
                    stateRate = GetStateRate(State.Virginia);
                    stateBracket = GetStateBracket(State.Virginia);
                    stateTax = TaxCalculator(stateRate, stateBracket, (income + capitolGains) - stateDeduction) - TaxCalculator(stateRate, stateBracket, income - stateDeduction);
                    break;
            }
            return federalTax + stateTax;       
        }

        public static double TaxCalculator(List<double> rate, List<double> bracket, double taxableIncome)
        {
            double taxes = 0;
            int currentBracket = 0;
            double incomeInBracket;

            //Find the maximum applicable tax bracket
            for (int i = 0; i < bracket.Count; i++)
            {
                if (taxableIncome > bracket[i])
                    currentBracket = i;
            }

            //run tax equation for each applicable tax bracket
            for (int i = currentBracket; i >=0; i--)
            {
                incomeInBracket = taxableIncome - bracket[i];
                taxes += incomeInBracket * rate[i];
                taxableIncome -= incomeInBracket;
            }

            return taxes;
        }

        private static double GetStateStandardDeduction(FilingStatus status, State state)
        {
            double stateDeduction = 0;

            //it looks silly with only one state but more states can be added to this
            switch (state)
            {
                case State.Virginia:
                {
                    if (status == FilingStatus.Joint)
                        stateDeduction = 6000;
                    else
                        stateDeduction = 3000;
                    break;
                }
            }

            return stateDeduction;
        }

        private static double GetFederalStandardDeduction(FilingStatus status, int age)
        {
            double federalDeduction = 0;

            switch (status)
            {
                case FilingStatus.Joint:
                    federalDeduction = 24000;
                    if (age >= 65) federalDeduction += 1600;
                    break;

                case FilingStatus.Single:
                    federalDeduction = 12000;
                    if (age >= 65) federalDeduction += 1300;
                    break;

                case FilingStatus.HeadOfHousehold:
                    federalDeduction = 18000;
                    if (age >= 65) federalDeduction += 1600;
                    break;

                case FilingStatus.Married:
                    federalDeduction = 12000;
                    if (age >= 65) federalDeduction += 1300;
                    break;
            }

            return federalDeduction;
        }

        private static List<double> GetStateRate(State state)
        {
            List<double> rate = new List<double> { };

            switch (state)
            {
                case State.Virginia:
                    rate = VirginiaRate;
                    break;
            }

            return rate;
        }

        private static List<double> GetStateBracket(State state)
        {
            List<double> bracket = new List<double> { };

            switch (state)
            {
                case State.Virginia:
                    bracket = VirginiaBracket;
                    break;
            }

            return bracket;
        }

        private static List<double> GetCapitolGainsBracket(FilingStatus status)
        {
            List<double> bracket = new List<double>();
            switch (status)
            {
                case FilingStatus.Joint:
                    bracket = new List<double> { 0, 77400, 480050 };
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

            return bracket;
        }
    }

    public enum FilingStatus //currently supported filing statuses
    {
        Single,
        Joint,
        HeadOfHousehold,
        Married
    }

    public enum State //currently supported states
    {
        Virginia
    }
}
