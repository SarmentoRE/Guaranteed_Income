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
        public List<Riders> riders { get; set; } //subject to change heavily 
        public FilingStatus filingStatus { get; set; }
        public string retirementDate { get; set; }
        public string deathDate { get; set; }
        public TaxBracket taxBracket { get; set; }
        public double assetIncome { get; set; }
        public State state { get; set; } = State.Virginia;
        public List<Concerns> concerns { get; set; }
        public double lumpSum { get; set; }
        public AssetType assetType { get; set; }
    
        public Person(InputModel model)
        {
            income = model.income;
            age = model.age;
            //concerns = model.concerns.Select(x => (Concerns)Enum.Parse(typeof(Concerns), x, true)).ToList();
            gender = (Gender)Enum.Parse(typeof(Gender), model.gender, true);
            filingStatus = (FilingStatus)Enum.Parse(typeof(FilingStatus), model.filingStatus, true);
            retirementDate = model.retirementDate;
            deathDate = model.deathDate;
            taxBracket = new TaxBracket(income, filingStatus, state, age);
            lumpSum = model.lumpSum;
            assetIncome = new AssetFactory(model.assets, retirementDate, deathDate, income).yearlyIncome;
            assetType = (AssetType)Int32.Parse(model.assets.assetHolder[0]);
        }
    }
}