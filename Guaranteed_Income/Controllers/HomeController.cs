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
            Person p1 = new Person(request);
        
            return JsonConvert.SerializeObject(p1);
        }
    }
}
