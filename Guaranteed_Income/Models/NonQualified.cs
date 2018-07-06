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
        }
    }
}