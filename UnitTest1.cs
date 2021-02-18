using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Lab1
{

    public class UnitTest1
    {

        public double myFunct(int x)
        {
            return (x - 6) / 4;
        }

        public static IEnumerable<TestCaseData> TestCases()

        {
            var testCases = new List<TestCaseData>();

            using (var fs = File.OpenRead("C:/Users/olesm/source/repos/AutoTest/Lab1/data.csv"))
            using (var sr = new StreamReader(fs))
            {
                string line = string.Empty;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] split = line.Split(new char[] { ',' },
                            StringSplitOptions.None);

                        int expectedNumber = Convert.ToInt32(split[0]);
                        decimal actualNumber = Convert.ToDecimal(split[1]);

                        var testCase = new TestCaseData(expectedNumber).Returns(actualNumber);
                        testCases.Add(testCase);
                    }
                }

                return testCases;
            }
        }

        [TestCaseSource("TestCases")]
        public double CalculateNumber(int x)
        {
            // arrange

            var xCalculate = new UnitTest1();

            // act

            double actualNumber = xCalculate.myFunct(x);

            // assert

            return actualNumber;
        }

    }
}
