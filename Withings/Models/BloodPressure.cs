﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.Models
{
    class BloodPressure
    {
        public int Diastolic { get; set; }
        public long LogId { get; set; }
        public int Systolic { get; set; }
        public DateTime Time { get; set; }
    }
}