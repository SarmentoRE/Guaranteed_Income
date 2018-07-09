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
        private NonDefFix nonDefFix;
        private NonDefVar nonDefVar;
        private NonImFix nonImFix;
        private NonImVar nonImVar;

        public NonQualified(Brokerage stock, Person person)
        {
            nonDefFix = new NonDefFix(person, stock);
            nonDefVar = new NonDefVar(person, stock);
            nonImFix = new NonImFix(person, stock);
            nonImVar = new NonImVar(person, stock);

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