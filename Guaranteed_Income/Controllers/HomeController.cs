using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Guaranteed_Income.Models;

namespace Guaranteed_Income.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class HomeController : Controller
    {
       [HttpPost]
       public JsonResult Post([FromBody]InputModel request)
        {
            var response = request;
            return Json(response);
        }
    }
}
