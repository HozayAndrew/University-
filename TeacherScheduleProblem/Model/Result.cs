using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Result
    {
        [JsonProperty(PropertyName = "studen_sequence")]
        public List<int> StudentSequence { get; set; }

        [JsonProperty(PropertyName = "sum_time")]
        public int SumTime { get; set; }

        [JsonProperty(PropertyName = "algorithm_time")]
        public double AlgorithmTime { get; set; }
    }
}
