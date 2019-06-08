using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SaveFile
    {
        [JsonProperty(PropertyName = "input_data")]
        public Data InputData { get; set; }

        [JsonProperty(PropertyName = "output_data")]
        public Result OutputData { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
