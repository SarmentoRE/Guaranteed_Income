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
        private QualDefFix qualDefFix;
        private QualDefVar qualDefVar;
        private QualImFix qualImFix;
        private QualImVar qualImVar;

        public Qualified(MonteCarlo carlo, Person person)
        {
            qualDefFix = new QualDefFix(person);
            qualDefVar = new QualDefVar(person);
            qualImFix = new QualImFix(person);
            qualImVar = new QualImVar(person);

            fixedIm = qualImFix.GetYearlyBreakdown(carlo);
            fixedDef = qualDefFix.GetYearlyBreakdown(carlo);
            varIm = qualImVar.GetYearlyBreakdown(carlo);
            varDef = qualDefVar.GetYearlyBreakdown(carlo);

            fixedImYearly = qualImFix.afterTaxIncome;
            fixedDefYearly = qualDefFix.afterTaxIncome;
            varImYearly = qualImVar.afterTaxIncome;
            varDefYearly = qualDefVar.afterTaxIncome;
        }
    }
}