using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.Portable.Models
{
    class LifetimeTotals
    {
        [JsonProperty("heartRate")]
        public int HeartRate { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
