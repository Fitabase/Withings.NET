using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace WithingsTest.Models
{
    public class BodyMeasure
    {
        public DateTime updatetime { get; set; }
        public string timezone { get; set; }
        public int more { get; set; }
        //public ApiCollectionType.measuregrps { get; set; }
        public string grpid { get; set; }
        public string attrib { get; set; }
        public DateTime date { get; set; }
        public int category { get; set; }
        //public ApiCollectionType.measures { get; set; }
        public int value { get; set; }
        public int unit { get; set; }
        public int type { get; set; }
        public string status { get; set; }
        //public Dictionary<body> { get; set; }

    }
}