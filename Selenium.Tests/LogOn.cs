using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Tests
{
    [TestClass]

    public class LogOn
    {
        [TestMethod]
        [TestCategory("Chrome")]
        public void Test_01_LogOn()
        {
            Assert.AreEqual(2, 2);
        }
    }
}
