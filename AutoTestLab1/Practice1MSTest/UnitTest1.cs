using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Practice1MsTest
{

    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public int CalculateFormula(int x)
        {
            return (x - 6) / 4;
        }


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "C:/Users/olesm/source/repos/AutoTestLab1/data.csv", "Data#csv",
        DataAccessMethod.Sequential)]
        [TestMethod()]
        public void CalculateNumber_FromDataSourceTest()
        {
            // Access the data
            int x = Convert.ToInt32(TestContext.DataRow[0]);
            int expected = CalculateFormula(x);
            int actual = Convert.ToInt32(TestContext.DataRow[1]);
            Assert.AreEqual(expected, actual);
        }


    }
}
