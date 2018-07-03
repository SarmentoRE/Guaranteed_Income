using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class Brokerage
    { 
        private MonteCarlo carlo;

        //Some variable asset with a confidence level
        public List<double> confident25 { get; set; }
        public List<double> confident50 { get; set; }
        public List<double> confident75 { get; set; }
        public List<double> confident90 { get; set; }

        public Brokerage(MonteCarlo carlo)
        {
            this.carlo = carlo;
            GenerateOutput();
        }


        private void GenerateOutput()
        {
            Confidence confidence = new Confidence(carlo.trialsList);

            confident25 = confidence.FindInterval(1);
            confident50 = confidence.FindInterval(50);
            confident75 = confidence.FindInterval(75);
            confident90 = confidence.FindInterval(90);
        }
    }
}
