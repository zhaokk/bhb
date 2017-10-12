using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using browserHistoryBrowser;

namespace UnitTestProject3
{
    [TestClass]
    public class CopyFileTester
    {
        [TestMethod]
        public void TestCopyFile()
        {
            var ieHistory = new IEHistory();
            ieHistory.CopyHistoryFile();
        }
    }
}
