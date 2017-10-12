using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace browserHistoryBrowser
{
    public class IEHistory:IWebsiteHistorySQLFunction
    {
        
        private DBReader _m_db;
        private List<Url> History;
        public string RunningDir;
        private string _fileLocation;
        private Lazy<List<string>> _m_tables;
        public bool CopyHistoryFile()
        {
            unlockDbFile();
            string userName = Environment.UserName;
            string baseAddress = @"C:\Users\{0}\AppData\Local\Microsoft\Windows\WebCache\WebCacheV01.dat";
            string directory = string.Format(baseAddress, userName);
            RunningDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _fileLocation = RunningDir + @"\WebCacheV01.dat";
            try
            {
                File.Copy(directory, _fileLocation, true);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public void unlockDbFile()
        {
            int count1 = Process.GetProcesses().
                                 Where(pr => pr.ProcessName == "dllhost").Count();
            Helper.KillProcessByName("dllhost");
            int count = Process.GetProcesses().
                                 Where(pr => pr.ProcessName == "dllhost").Count();
            if (count>0)
            {
                Helper.KillProcessByName("dllhost");
            }
        }
        public void RetriveHistory()
        {
            var recoveryEnabled = true;
            _m_db = new DBReader(_fileLocation);
            _m_db.Init(recoveryEnabled);
            _m_tables = new Lazy<List<string>>(() => new List<string>(_m_db.Tables));
        }
        public List<Url> GetHistory(int langId)
        {
            CopyHistoryFile();
            try
            {
                RetriveHistory();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:");
                Console.WriteLine("Exception happened while retrive data form IE exception message: "+e.Message);
            }
            ConvertUrlsToCsv(History);
            return new List<Url>();
        }
        public void ConvertUrlsToCsv(List<Url> urls)
        {
            if (urls==null)
            {
                Console.WriteLine("IE history not available");
                Console.ReadLine();
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("title,url,visitCount,lastVisstTime");
                foreach (var data in urls)
                {
                    sb.AppendLine(data.title + "," + data.url + ", " + data.visitCount + ", " + data.lastVisitTime);
                }
                string filePath = RunningDir + @"\result.csv";
                File.WriteAllText(filePath, sb.ToString());
            }
        }
    }
}
