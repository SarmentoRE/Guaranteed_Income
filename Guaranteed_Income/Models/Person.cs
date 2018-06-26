using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class Person
    {
        private int income { get; set; }

        private int age { get; set; }

        private String gender { get; set; }

        private List<Concerns> concerns { get; set; } //subject to chage heavily 

        private int additions { get; set; }

        private FilingStatus filingStatus { get; set; }

        private String retirementDate { get; set; }

        private String deathDate { get; set; }

        private TaxBracket taxBracket { get; set; }

        public Person(InputModel model)
        {
            this.income = model.income;
            this.age = model.age;
            //this.concerns = StringToConcerns(model.concerns)
            this.gender = model.gender;
            this.additions = model.additions;
            this.filingStatus = (FilingStatus)Enum.Parse(typeof(FilingStatus),model.filingStatus ,true);
            this.retirementDate = model.retirementDate;
            this.deathDate = model.deathDate;
            this.taxBracket = new TaxBracket(this.income, this.filingStatus, State.Virginia);
        }
    }
}
