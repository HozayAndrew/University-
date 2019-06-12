using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model;
using TeacherProblem.Helpers;

namespace TeacherProblem.Controllers
{
    [Route("api/[controller]")]
    public class ExperimentsController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Normal request";
        }

        [HttpPost]
        public IActionResult Get([FromBody] ExperimentsSettings settings)
        {
            List<Data> data = new List<Data>();
            var currentValue = settings.StartCount;

            var generator = new Generator();
            while (currentValue < settings.FinishCount)
            {
                for (int i = 0; i < settings.Count; i++)
                {
                    var tempData = generator.GetData(currentValue);
                    data.Add(tempData);
                }
                currentValue += settings.Step;
            }

            return new JsonResult(data);
        }

        [HttpPost("greedy")]
        public IActionResult GreedyPost([FromBody] List<Data> data)
        {
            var exp = new Experiments();
            var result = exp.GetExperimentResults(new GreedyAlgorithm(), data);
            return new JsonResult(result);
        }

        [HttpPost("antColony")]
        public IActionResult AntColonyPost([FromBody] List<Data> data)
        {
            var exp = new Experiments();
            var result = exp.GetExperimentResults(new AntColonyOptimizationAlgorithms(), data);
            return new JsonResult(result);
        }
    }
}