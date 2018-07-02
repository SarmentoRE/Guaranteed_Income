namespace Guaranteed_Income.Interfaces
{
    public abstract class Annuities
    {
        public double minPremium = 56918; //the average minimum premium amoung insurance companies
        public double rate = 0.0311275; //the average return rate amoung insurance companies
        public double initialAmount;
        public double extraPayments;
        public double accumulationYears;
        public double growthPercentage;
        public double lumpSum;
        public double extraFees;
        public double addedRider;
        public double yearsAfterRetirement;
        public double distributionsBeforeTax;
        public double exclusionRatio;

    }
}
