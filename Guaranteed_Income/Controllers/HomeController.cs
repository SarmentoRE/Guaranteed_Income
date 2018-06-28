using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Guaranteed_Income.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Guaranteed_Income.Services;
using Guaranteed_Income.Utilities;

namespace Guaranteed_Income.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Get()
        {
            MonteCarlo x = new MonteCarlo(30000, 0.0083, .0424, 360);

            return JsonConvert.SerializeObject(x.GetTrialsList());
        }

        [HttpPost]
        public JToken Post([FromBody] InputModel request)
        {
            MonteCarlo x = new MonteCarlo(30000, 0.0083, .0424, 360);
            return JsonConvert.SerializeObject(x.GetTrialsList());
        }
    }
}
