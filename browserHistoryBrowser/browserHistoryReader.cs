using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace browserHistoryBrowser
{
    class browserHistoryReader
    {
        public void Start()
        {
            Console.WriteLine("hello world");
            ChromeHistory ch = new ChromeHistory();
            ch.GetHistory(0);
            IEHistory csqlh = new IEHistory();
            csqlh.GetHistory(0);
            // write code here that runs when the Windows Service starts up.  
        }
        public void Stop()
        {
            // write code here that runs when the Windows Service stops.  
        }
    }
}
