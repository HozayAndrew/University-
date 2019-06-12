using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace TeacherProblem.Controllers
{
    [Route("api/[controller]")]
    public class AlgorithmsController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/algorithms
        [EnableCors("AllowMyOrigin")]
        [HttpPost("greedy")]
        public IActionResult GreedyPost([FromBody] Data data)
        {
            if (data.Count <= 0) return BadRequest("Count <= 0");
            else if (data.Time.Count != data.Count) return BadRequest("Time.Count != Count");
            else if (data.Matrix.Count != data.Count) return BadRequest("Matrix.Count != Count");

            for (int i = 0; i < data.Matrix.Count; i++)
            {
                var list = data.Matrix[i];
                if (list.Count != data.Count) return BadRequest($"Matrix[{i}].Count != Count");
            }

            var algorithm = new GreedyAlgorithm();
            var result = algorithm.RunAlgorithm(data);

            return new JsonResult(result);
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost("antColony")]
        public IActionResult AntColonyPost([FromBody] Data data)
        {
            if (data.Count <= 0) return BadRequest("Count <= 0");
            else if (data.Time.Count != data.Count) return BadRequest("Time.Count != Count");
            else if (data.Matrix.Count != data.Count) return BadRequest("Matrix.Count != Count");

            for (int i = 0; i < data.Matrix.Count; i++)
            {
                var list = data.Matrix[i];
                if (list.Count != data.Count) return BadRequest($"Matrix[{i}].Count != Count");
            }

            var algorithm = new AntColonyOptimizationAlgorithms();

            Result result = null;
            try
            {
                result = algorithm.RunAlgorithm(data);
            }
            catch(Exception ex)
            {
                //lnjk
            }

            return new JsonResult(result);
        }
    }
}