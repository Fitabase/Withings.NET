using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithingsTests.Models
{
    public class Device
    {
        [JsonProperty("battery")]
        public string Battery { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastSynchTime")]
        public DateTime LastSynchTime { get; set; }

    }
}
