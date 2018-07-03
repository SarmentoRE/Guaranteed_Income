using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Utilities
{
    public class FutureValue
    {
        public double cashFlow { get; set; }
        public double rate { get; set; }
        public double periods { get; set; }

        public FutureValue(double cashFlow, double rate, double periods)
        {
            this.cashFlow = cashFlow;
            this.rate = rate;
            this.periods = periods;
        }

        public double GetFutureValue()
        { 
            double value = cashFlow * (Math.Pow( (1 + rate), periods));
            return value;
        }
    }
}
