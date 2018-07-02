using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class Person
    {
        public double income { get; set; }
        public int age { get; set; }
        public Gender gender { get; set; }
        public List<Concerns> concerns { get; set; } //subject to change heavily 
        public int additions { get; set; }
        public FilingStatus filingStatus { get; set; }
        public string retirementDate { get; set; }
        public string deathDate { get; set; }
        public TaxBracket taxBracket { get; set; }
        public List<IAsset> Assets { get; set; }
        public State state { get; set; } = State.Virginia;
    
        public Person(InputModel model)
        {
            this.income = model.income;
            this.age = model.age;
            //this.concerns = StringToConcerns(model.concerns)
            this.gender = (Gender)Enum.Parse(typeof(Gender), model.gender, true);
            this.additions = model.additions;
            this.filingStatus = (FilingStatus)Enum.Parse(typeof(FilingStatus), model.filingStatus, true);
            this.retirementDate = model.retirementDate;
            this.deathDate = model.deathDate;
            this.taxBracket = new TaxBracket(this.income, this.filingStatus, this.state, this.age);
        }
    }
}