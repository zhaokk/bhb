using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace browserHistoryBrowser
{
    interface IWebsiteHistorySQLFunction
    {
        List<Url> GetHistory(int langId);
    }
}
