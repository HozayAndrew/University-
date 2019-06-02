using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace TeacherProblem.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    public class ExperimentsController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost("greedy")]
        public IActionResult GreedyPost([FromBody] ExperimentsData data)
        {
            var exp = new Experiments();
            var result = exp.GetExperimentResults(new GreedyAlgorithm(), data);
            return new JsonResult(result);
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost("antColony")]
        public IActionResult AntColonyPost([FromBody] ExperimentsData data)
        {
            var exp = new Experiments();
            var result = exp.GetExperimentResults(new AntColonyOptimizationAlgorithms(), data);
            return new JsonResult(result);
        }
    }
}