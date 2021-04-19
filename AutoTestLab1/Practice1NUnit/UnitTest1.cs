using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Lab1
{
    public class UnitTest1
    {

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Console.WriteLine("Before tests");
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Console.WriteLine("After tests");
        }
        public double myFunct(int x)
        {
            return (x - 6) / 4;
        }

        public double FailedFunc(int a)
        {
            try
            {
                int b = 0;
                int c = a / b;
                return c;
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("There was error in your formula", e);
                return 0;
            }
        }

        public static IEnumerable<TestCaseData> TestCases()

        {
            var testCases = new List<TestCaseData>();

            using (var fs = File.OpenRead("C:/Users/olesm/source/repos/AutoTestLab1/data.csv"))
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
                        Console.WriteLine(testCase);
                        testCases.Add(testCase);
                    }
                }

                return testCases;
            }
        }

        public static IEnumerable<int> TestCasesRandomData()
        {
            var testCases = new List<int>();
            int n = 10;
            var rand = new Random(73247823);

            for (int i = 0; i < n + 1; i++)
            {
                testCases.Add(rand.Next(0, 100));
            }

            return testCases;
        }

        public static IEnumerable<int> TestCasesFailed()
        {
            var testCases = new List<int>();
            int n = 10;
            var rand = new Random(73247823);

            for (int i = 0; i < n + 1; i++)
            {
                testCases.Add(rand.Next(0, 100));
            }

            return testCases;
        }


        [TestCaseSource("TestCases")]

        public double CalculateNumber(int x)
        {

            Console.WriteLine("Some text here");
            // arrange

            var xCalculate = new UnitTest1();

            // act

            double result = xCalculate.myFunct(x);

            // assert

            return result;
        }

        [TestCaseSource("TestCasesRandomData")]
        public void CalculateRandomNumber(int x)
        {
            // arrange

            var xCalculate = new UnitTest1();

            // act

            double result = xCalculate.myFunct(x);

            // assert

            Assert.That(result > 0);
        }

        [TestCaseSource("TestCasesFailed")]
        public void FailedTest(int x)
        {
            // arrange

            var xCalculate = new UnitTest1();

            // act

            double result = xCalculate.myFunct(x);

            // assert

            Assert.That(result > 0);
        }

    }
}
