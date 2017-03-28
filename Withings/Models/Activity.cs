using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.Models
{
    class Activity
    {
       public DateTime Date { get; set; }
       public string TimeZone { get; set; } 
       public int Steps { get; set; }
       public float Distance { get; set; }
       public float Calories { get; set; }
       public float TotalCalories { get; set; }
       public float Elevation { get; set; }
       public float Soft { get; set; }
       public int Moderate { get; set; }
       public int Intense { get; set; }
       public string Status { get; set; }
       
    }
}
