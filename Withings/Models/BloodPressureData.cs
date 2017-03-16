using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.Models
{
    class BloodPressureData
    {
        public BloodPressureAverage Average { get; set; }
        public List<BloodPressure> BP { get; set; }
    }
}
