using Guaranteed_Income.Interfaces;
using Guaranteed_Income.Models.Annuity;
using Guaranteed_Income.Services;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class Qualified
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
        private QualDefFix qualDefFix;
        private QualDefVar qualDefVar;
        private QualImFix qualImFix;
        private QualImVar qualImVar;

        public Qualified(Brokerage stock, Person person)
        {
            qualDefFix = new QualDefFix(person, stock);
            qualDefVar = new QualDefVar(person, stock);
            qualImFix = new QualImFix(person, stock);
            qualImVar = new QualImVar(person, stock);

            fixedIm = qualImFix.yearlyBreakdown;
            fixedDef = qualDefFix.yearlyBreakdown;
            varIm = qualImVar.yearlyBreakdown;
            varDef = qualDefVar.yearlyBreakdown;

            fixedImYearly = qualImFix.afterTaxIncome;
            fixedDefYearly = qualDefFix.afterTaxIncome;
            varImYearly = qualImVar.afterTaxIncome;
            varDefYearly = qualDefVar.afterTaxIncome;

            fixedDefAsset = qualDefFix.assetValue;
            fixedImAsset = qualImFix.assetValue;
            varDefAsset = qualDefVar.assetValue;
            varImAsset = qualImVar.assetValue;

            Annuities.FinishStock(stock);
        }
    }
}