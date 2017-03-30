using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.Models
{
    public class MeasureGroup
    {
        [JsonProperty(PropertyName ="grpid")]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "attrib")]
        public string Attribute { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "category")]
        public int Category { get; set; }
        public IEnumerable<Measure> Measures { get; set; }

    }
}
