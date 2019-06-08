using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ExperimentResult
    {
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "time")]
        public double Time { get; set; }

        [JsonProperty(PropertyName = "function_result")]
        public int FunctionResult { get; set; }
    }
}
