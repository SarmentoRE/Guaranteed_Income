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
        private NonDefFix nonDefFix;
        private NonDefVar nonDefVar;
        private NonImFix nonImFix;
        private NonImVar nonImVar;

        public NonQualified(MonteCarlo carlo, Person person)
        {
            nonDefFix = new NonDefFix(person);
            nonDefVar = new NonDefVar(person);
            nonImFix = new NonImFix(person);
            nonImVar = new NonImVar(person);

            fixedIm = nonImFix.GetYearlyBreakdown(carlo);
            fixedDef = nonDefFix.GetYearlyBreakdown(carlo);
            varIm = nonImVar.GetYearlyBreakdown(carlo);
            varDef = nonDefVar.GetYearlyBreakdown(carlo);
        }
    }
}