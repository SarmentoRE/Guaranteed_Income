using Microsoft.AspNetCore.Mvc;
using Guaranteed_Income.Models;

namespace Guaranteed_Income.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class HomeController : Controller
    {
        /* Test
        [HttpGet]
        public JsonResult Get()
        {
            OutputModel output = new OutputModel();
            return Json(output);
        }
        */

        [HttpPost]
        public JsonResult Post([FromBody] InputModel request)
        {
            OutputModel output = new OutputModel(request);
            return Json(output);
        }
    }
}
