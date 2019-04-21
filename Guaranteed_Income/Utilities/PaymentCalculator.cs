using System;

namespace Guaranteed_Income.Utilities
{
    public static class PaymentCalculator
    {
        public static double GetPayments(double initialAmount, double rate, double periods)
        {
            var paymentAmount = (rate * initialAmount) / (1 - Math.Pow(1 + rate, periods * -1));

            return paymentAmount;
        }
    }
}
