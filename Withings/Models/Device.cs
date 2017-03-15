using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.Models
{
    class Device
    {
        [JsonProperty("battery")]
        public string Battery { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastSyncTime")]
        public DateTime LastSyncTime { get; set; }

        [JsonProperty("type")]
        public DeviceType Type { get; set; }

        [JsonProperty("deviceVersion")]
        public string DeviceVersion { get; set; } // todo: this should be an enum; "Flex|One|Zip|Ultra|Classic|Aria"

        [JsonProperty("mac")]
        public string Mac { get; set; }
    }
}
