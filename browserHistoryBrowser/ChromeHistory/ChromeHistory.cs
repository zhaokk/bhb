using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;


namespace browserHistoryBrowser
{
    class ChromeHistory:IWebsiteHistorySQLFunction
    {
        private string _connectionString = "Data Source=History.db;Version=3;New=False;Compress=True;";
        private SQLiteConnection _sql_con;
        private SQLiteCommand _sql_cmd;
        private SQLiteDataAdapter _DB;
        private DataSet _DS = new DataSet();
        private DataTable _DT = new DataTable();
        private string _runningDir;
        public bool CopyHistoryFile()
        {
            string userName = Environment.UserName;
            string baseAddress = @"C:\Users\{0}\AppData\Local\Google\Chrome\User Data\Default\history";
            string directory = string.Format(baseAddress, userName);
            _runningDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string location = _runningDir +@"\history.db";
            File.Copy(directory, location, true);
            return true;
        }
        public void SetConnection()
        {
            _sql_con = new SQLiteConnection
                ("Data Source=History.db;Version=3;New=False;Compress=True;");
        }
        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            _sql_con.Open();
            _sql_cmd = _sql_con.CreateCommand();
            _sql_cmd.CommandText = txtQuery;
            _sql_cmd.ExecuteNonQuery();
            _sql_con.Close();
        }
        public void LoadData()
        {
            SetConnection();
            _sql_con.Open();
            _sql_cmd = _sql_con.CreateCommand();
            string CommandText = "select id, title from urls";
            _DB = new SQLiteDataAdapter(CommandText, _sql_con);
            _DS.Reset();
            _DB.Fill(_DS);
            _DT = _DS.Tables[0];
            _sql_con.Close();
        }
      
        public List<Url> GetHistory(int langId)
        {
            CopyHistoryFile();
            List<Url> urls = new List<Url>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT id,url,title,visit_count, datetime(last_visit_time / 1000000 + (strftime('%s', '1601-01-01')), 'unixepoch') as last_visit_time FROM Urls WHERE Id = " + langId;
                    if (langId == 0)
                    {
                        sql = "SELECT id,url,title,visit_count, datetime(last_visit_time / 1000000 + (strftime('%s', '1601-01-01')), 'unixepoch') as last_visit_time FROM Urls";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Url la = new Url();
                                la.id = Int32.Parse(reader["Id"].ToString());
                                la.title = reader["title"].ToString();
                                la.visitCount = reader["visit_count"].ToString();
                                la.lastVisitTime = reader["last_visit_time"].ToString();
                                la.LVTinClass = Convert.ToDateTime(la.lastVisitTime);
                                //DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                                //dateTime = dateTime.AddMilliseconds(dateTimeInt / 10000);
                                //la.lastVisitTime = dateTime.ToString();
                                la.url = reader["url"].ToString();
                                urls.Add(la);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (SQLiteException e)
            {
                throw e;
            }
           
            List<Url> SortedList = urls.OrderByDescending(o => o.LVTinClass).ToList();
            Helper.ConvertUrlsToCsv(SortedList, _runningDir);
            return urls;
        }
     
    }
}
