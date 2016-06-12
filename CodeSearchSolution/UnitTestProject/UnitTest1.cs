using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBed;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private Utility _utility = new Utility();
        [TestMethod]
        public void TestCamelToSpace()
        {
            Assert.IsTrue(_utility.CovertCamelCaseToSpaceSeparted("addTwoItem()")=="add two item ( )");
        }
    }
}
