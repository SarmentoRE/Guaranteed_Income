using Guaranteed_Income.Models.Annuity;
using Guaranteed_Income.Services;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class NonQualified
    {
        public List<List<double>> fixedIm;
        public List<List<double>> fixedDef;
        public List<List<double>> varIm;
        public List<List<double>> varDef;
        public double fixedImYearly;
        public double fixedDefYearly;
        public double varImYearly;
        public double varDefYearly;
        public double fixedImAsset;
        public double fixedDefAsset;
        public double varImAsset;
        public double varDefAsset;
        private AnnuityFactory nonDefFix;
        private AnnuityFactory nonDefVar;
        private AnnuityFactory nonImFix;
        private AnnuityFactory nonImVar;

        public NonQualified(Brokerage stock, Person person)
        {
            nonDefFix = new AnnuityFactory(AnnuityTax.Nonqualified, AnnuityTime.Deferred, AnnuityRate.Fixed, person, stock);
            nonDefVar = new AnnuityFactory(AnnuityTax.Nonqualified, AnnuityTime.Deferred, AnnuityRate.Variable, person, stock);
            nonImFix = new AnnuityFactory(AnnuityTax.Nonqualified, AnnuityTime.Immediate, AnnuityRate.Fixed, person, stock);
            nonImVar = new AnnuityFactory(AnnuityTax.Nonqualified, AnnuityTime.Immediate, AnnuityRate.Variable, person, stock);

            fixedIm = nonImFix.yearlyBreakdown;
            fixedDef = nonDefFix.yearlyBreakdown;
            varIm = nonImVar.yearlyBreakdown;
            varDef = nonDefVar.yearlyBreakdown;

            fixedImYearly = nonImFix.afterTaxIncome;
            fixedDefYearly = nonDefFix.afterTaxIncome;
            varImYearly = nonImVar.afterTaxIncome;
            varDefYearly = nonDefVar.afterTaxIncome;

            fixedDefAsset = nonDefFix.assetValue;
            fixedImAsset = nonImFix.assetValue;
            varDefAsset = nonDefVar.assetValue;
            varImAsset = nonImVar.assetValue;
        }
    }
}