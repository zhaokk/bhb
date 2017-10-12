using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace browserHistoryBrowser
{
    public static class Helper
    {
        // subfix like .exe is not part of the process name
        public static void KillProcessByName(string name)
        {
            var Processes = Process.GetProcesses().
                                 Where(pr => pr.ProcessName == name);
            var AllProcesses = Process.GetProcesses();
            foreach (var process in Processes)
            {
                process.Kill();
            }
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static void ConvertUrlsToCsv(List<Url> urls,string runningDir)
        {
            var sb = new StringBuilder();
            sb.AppendLine("title,url,visitCount,lastVisstTime");
            int i = 0;
            Console.WriteLine("The first 30 histories");
            foreach (var data in urls)
            {
                string row = data.title + "," + data.url + ", " + data.visitCount + ", " + data.lastVisitTime;
                sb.AppendLine(row);
                i++;
                if (i < 30)
                {
                    Console.WriteLine("######################### " + i + " #########################");
                    Console.WriteLine(row);
                }

            }
            Console.WriteLine("....");
            Console.WriteLine(i + " records in total");
            string filePath = runningDir + @"\result.csv";
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
