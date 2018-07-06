using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class OutputModel
    {
        public Brokerage brokerage;
        public Qualified qualified;
        public NonQualified nonQualified;

        private Person person;
        private MonteCarlo carlo;
        private double expectedReturn = 0.0992;
        private double standardDeviation = 0.1468;
        private double time;

        public OutputModel()
        {
        }

        public OutputModel(InputModel input)
        {
            person = new Person(input);
            if (person.concerns[1]) expectedReturn = -0.0992;
            time = (person.retirementDate - DateTime.Now.Year);
            carlo = new MonteCarlo(person.lumpSum, expectedReturn, standardDeviation, time);
            brokerage = new Brokerage(carlo);
            qualified = new Qualified(carlo, person);
            nonQualified = new NonQualified(carlo, person);
        }
    }
}
