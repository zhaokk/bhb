using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace browserHistoryBrowser
{

    public class Url
    {
        public int id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string visitCount { get; set; }
        public int typedCount { get; set; }
        public string lastVisitTime { get; set; }
        public int hidden { get; set; }
        public DateTime LVTinClass { get; set; }
    }

}
