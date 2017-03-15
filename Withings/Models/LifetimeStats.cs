using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.Portable.Models
{
    class LifetimeStats
    {
        [JsonProperty("total")]
        public LifetimeTotals Total { get; set; }

        [JsonProperty("tracker")]
        public LifetimeTotals Tracker { get; set; }
    }
}
