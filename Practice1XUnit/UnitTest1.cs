using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Practice1xUnit
{

    public class Calculator
    {
        public int Formula(int x)
        {
            return (x - 6) / 4;
        }
    }

    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
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

                        int x = Convert.ToInt32(split[0]);
                        int expected = Convert.ToInt32(split[1]);

                        yield return new object[] { x, expected };
                    }
                }
                //yield return new object[] { 6, 0};
                //yield return new object[] { 14, 2};
                //yield return new object[] { 22, 4};
                //yield return new object[] { 30, 6};
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CalculatorTestRandomData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var rand = new Random(73247823);
            var n = 10;

            for (int i = 0; i < n + 1; i++)
            {
                yield return new object[] { rand.Next(0, 100) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CalculatorTests
    {
        //[Fact]

        [Theory]
        //[InlineData(6, 0)]
        //[InlineData(14, 2)]
        //[InlineData(22, 4)]
        [ClassData(typeof(CalculatorTestData))]
        public void CanFormulaTheory(int x, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Formula(x);

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(CalculatorTestRandomData))]
        public void CanFormulaRandomTheory(int x)
        {
            var calculator = new Calculator();

            var result = calculator.Formula(x);

            Assert.True(result > 0);
        }
    }
}
