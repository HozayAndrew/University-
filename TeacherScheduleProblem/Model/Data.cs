using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherProblem.Model
{
    public class Data
    {
        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "Time")]
        public List<int> Time { get; set; }

        [JsonProperty(PropertyName = "Matrix")]
        public List<List<int>> Matrix { get; set; }
    }
}
