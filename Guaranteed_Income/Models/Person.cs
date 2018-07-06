using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guaranteed_Income.Models
{
    public class Person
    {
        public double income { get; set; }
        public int age { get; set; }
        public Gender gender { get; set; }
        //public List<IRider> riders { get; set; } //subject to change heavily 
        public FilingStatus filingStatus { get; set; }
        public int retirementDate { get; set; }
        public int deathDate { get; set; }
        public TaxBracket taxBracket { get; set; }
        public double assetIncome { get; set; }
        public State state { get; set; } = State.Virginia;
        public List<bool> concerns { get; set; }
        public double lumpSum { get; set; }
        public AssetType assetType { get; set; }
    
        public Person(InputModel model)
        {
            income = model.income;
            age = model.age;
            gender = (Gender)Enum.Parse(typeof(Gender), model.gender, true);
            filingStatus = (FilingStatus)Enum.Parse(typeof(FilingStatus), model.filingStatus, true);
            retirementDate = Int32.Parse(model.retirementDate.Substring(0,4));
            deathDate = Int32.Parse(model.deathDate.Substring(0, 4));
            taxBracket = new TaxBracket(income, filingStatus, state, age);
            lumpSum = model.lumpSum;
            concerns = model.concerns;
            assetIncome = new AssetFactory(model.assets, retirementDate, deathDate, income).yearlyIncome;
            assetType = (AssetType)Enum.Parse(typeof(AssetType),model.assets.assets[0],true);
        }
    }
}