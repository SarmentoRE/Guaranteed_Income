using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;
using System.Collections.Generic;

namespace Guaranteed_Income.Models
{
    public class OutputModel
    {
        public Brokerage brokerage = new Brokerage();
        private Person person;

        public OutputModel() { }

        public OutputModel(InputModel input)
        {
           person = new Person(input);
           //if()
        }
    }
}
