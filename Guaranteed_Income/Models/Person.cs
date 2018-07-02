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
        public int additions { get; set; }
        public FilingStatus filingStatus { get; set; }
        public string retirementDate { get; set; }
        public string deathDate { get; set; }
        public TaxBracket taxBracket { get; set; }
        public List<IAsset> Assets { get; set; }
        public State state { get; set; } = State.Virginia;
        public List<Concerns> concerns { get; set; }
    
        public Person(InputModel model)
        {
            this.income = model.income;
            this.age = model.age;
            this.concerns = model.concerns.Select(x => (Concerns)Enum.Parse(typeof(Concerns), x, true)).ToList();
            this.gender = (Gender)Enum.Parse(typeof(Gender), model.gender, true);
            this.additions = model.additions;
            this.filingStatus = (FilingStatus)Enum.Parse(typeof(FilingStatus), model.filingStatus, true);
            this.retirementDate = model.retirementDate;
            this.deathDate = model.deathDate;
            this.taxBracket = new TaxBracket(this.income, this.filingStatus, this.state, this.age);
        }
    }
}