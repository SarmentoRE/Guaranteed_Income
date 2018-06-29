using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class OutputModel
    {

        private double currentValue = 30000;
        private double expectedReturn = 0.0083;
        private double standardDeviation = 0.0424;
        private double time = 360;

        //Some variable asset with a confidence level
        public List<double> confident25 { get; set; }
        public List<double> confident50 { get; set; }
        public List<double> confident75 { get; set; }
        public List<double> confident95 { get; set; }
 
        public OutputModel()
        {
            GenerateOutput();
        }

        public OutputModel(double currentValue, double expectedReturn, double standardDeviation, double time)
        {
            this.currentValue = currentValue;
            this.expectedReturn = expectedReturn;
            this.standardDeviation = standardDeviation;
            this.time = time;

            GenerateOutput();
        }

        private void GenerateOutput()
        {
            MonteCarlo monteCarlo = new MonteCarlo(currentValue, expectedReturn, standardDeviation, time);
            Confidence confidence = new Confidence(monteCarlo.trialsList);

            confident25 = confidence.FindInterval(25);
            confident50 = confidence.FindInterval(50);
            confident75 = confidence.FindInterval(75);
            confident95 = confidence.FindInterval(95);
        }
    }
}
