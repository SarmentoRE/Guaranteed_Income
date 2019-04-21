using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Utilities
{
    public static class FutureValue
    {
        public static double GetFutureValue(double cashFlow, double rate, double periods)
        {
            double value = cashFlow;
            for (int i = 0; i < periods; i++)
            {
                value += (value * rate);
            }
            return value;
        }
    }
}
