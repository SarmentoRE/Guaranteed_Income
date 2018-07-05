using Guaranteed_Income.Models.Annuity;
using Guaranteed_Income.Services;

namespace Guaranteed_Income.Models
{
    public class Qualified
    {

        public double fixedIm;
        public double fixedDef;
        public double varIm;
        public double varDef;
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
        }
    }
}