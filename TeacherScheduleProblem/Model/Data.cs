using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Data
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "time")]
        public List<int> Time { get; set; }

        [JsonProperty(PropertyName = "matrix")]
        public List<List<int>> Matrix { get; set; }
    }
}
