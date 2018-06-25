using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Guaranteed_Income.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Guaranteed_Income.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpPost]
        public JToken Post([FromBody] InputModel request)
        {
            var p1 = request.personOne;
            List<Person> personList = MakePersons(request);
            Person client = personList[0];
        
            return JsonConvert.SerializeObject(client);
        }

        public List<Person> MakePersons(InputModel model)
        {
            List<Person> personList = new List<Person>();
            Person p1 = new Person();
            Person p2 = new Person();

            p1.income = (int)model.personOne.Property("income");
            p1.age = (int)model.personOne.Property("age");

            if (model.personTwo != null)
            {
                p2.income = (int)model.personOne.Property("income");
                p2.age = (int)model.personTwo.Property("age");

                personList.Add(p2);
            }

            personList.Add(p1);

            return personList;
        }
    }
}
