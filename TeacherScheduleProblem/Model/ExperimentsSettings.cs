using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ExperimentsSettings
    {
        [JsonProperty(PropertyName = "start_count")]
        public int StartCount { get; set; }

        [JsonProperty(PropertyName = "finish_count")]
        public int FinishCount { get; set; }

        [JsonProperty(PropertyName = "step")]
        public int Step { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
