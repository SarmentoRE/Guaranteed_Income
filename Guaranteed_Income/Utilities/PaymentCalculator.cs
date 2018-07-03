using System;

namespace Guaranteed_Income.Utilities
{
    public class PaymentCalculator
    {
        public double initialAmount;
        public double rate;
        public double periods;

        public PaymentCalculator(double initialAmount, double rate, double periods)
        {
            this.initialAmount = initialAmount;
            this.rate = rate;
            this.periods = periods;
        }

        public double GetPayments()
        {
            var paymentAmount = (rate * initialAmount) / (1 - Math.Pow(1 + rate, periods * -1));
            return paymentAmount;
        }
    }
}
