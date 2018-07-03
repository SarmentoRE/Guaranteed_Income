using Microsoft.AspNetCore.Mvc;
using Guaranteed_Income.Models;

namespace Guaranteed_Income.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            //MonteCarlo x = new MonteCarlo(30000, 0.0083, .0424, 360);
            //MonteCompare comparer = new MonteCompare();
            //StoParallelMergeSort<List<double>> mergeSort = new StoParallelMergeSort<List<double>>(comparer);
            //mergeSort.Sort(x.trialsList);
            //return "Done";
            OutputModel output = new OutputModel();
            return Json(output);
        }

        [HttpPost]
        public JsonResult Post([FromBody] InputModel request)
        {
            OutputModel output = new OutputModel(request);
            return Json(output);
        }
    }
}
