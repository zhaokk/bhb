using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using browserHistoryBrowser;

namespace UnitTestProject3
{
    [TestClass]
    public class KillProcessByNameTester
    {
        [TestMethod]
        public void TestKillProcess()
        {
            string processName = "dllhost.exe";
            Helper.KillProcessByName(processName);
        }
    }
}
