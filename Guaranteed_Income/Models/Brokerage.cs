using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
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

        public Brokerage(MonteCarlo carlo)
        {
            this.carlo = carlo;
            GenerateOutput();
        }


        private void GenerateOutput()
        {
            Confidence confidence = new Confidence(carlo.trialsList);
            double growth75;
            double growth90;

            confident75 = confidence.FindInterval(75);
            confident90 = confidence.FindInterval(90);
            amountAtRetirement75 = confident75[confident75.Count - 1];
            amountAtRetirement90 = confident90[confident90.Count - 1];

            growth75 = (amountAtRetirement75 - confident75[0])/confident75[0];
            growth90 = (amountAtRetirement90 - confident90[0]) / confident90[0];

        }
    }
}
