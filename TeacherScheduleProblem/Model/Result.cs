using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherProblem.Model
{
    public class Result
    {
        [JsonProperty(PropertyName = "StudentSequence")]
        public List<int> StudentSequence { get; set; }

        [JsonProperty(PropertyName = "SumTime")]
        public int SumTime { get; set; }

        [JsonProperty(PropertyName = "AlgorithmTime")]
        public int AlgorithmTime { get; set; }
    }
}
