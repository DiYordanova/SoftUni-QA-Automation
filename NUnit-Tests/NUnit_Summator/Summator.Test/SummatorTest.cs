using NUnit.Framework;
using System;
using System.Numerics;

namespace Summator.Test
{
 
    public class SummatorTests
    {
        private Summator summator;

        [SetUp]
        public void SetUp()
        {
            summator = new Summator();
            Console.WriteLine("Test started.");
        }

        [Test]
        public void Sum_TwoPositiveNumber()
        {
            double actual = summator.Sum(new double[] { 10, 6 });
            int expected = 16;
            Assert.That(actual == expected);
        }

        [Test]
        public void Sum_OnePositiveNumber()
        {
            double actual = summator.Sum(new double[] { 7 });
            Assert.That(actual == 7);
        }

        [Test]
        public void Sum_TwoNegativeNumber()
        {
            double actual = summator.Sum(new double[] { -10, -6 });            
            Assert.That(actual == -16);
        }

        [Test]
        public void Sum_DoubleNumbers()
        {
            double actual = summator.Sum(new double[] { 2.5, 3.6 });
            Assert.That(actual == 6.1);
        }

        [Test]
        public void Sum_EmptyArray()
        {
            double actual = summator.Sum(new double[] {});            
            Assert.That(actual == 0);
        }

        [Test]
        public void Sum_BigValues()
        {
            double actual = summator.Sum(new double[] { 2000000000, 2000000000, 2000000000 });
            Assert.AreEqual(6000000000, actual);
        }

        [Test]
        public void Substraction_OnePositiveNumber()
        {
            long actual = summator.SubstractionOneNumber(10, 2);
            int expected = 8;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Substraction_NegativeMinuendAndSubtrahend()
        {
            long actual = summator.SubstractionOneNumber(-100, -60);
            int expected = -40;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Substraction_NegativeMinuendAndSubtrahend_BigValues()
        {
            long actual = summator.SubstractionOneNumber(-3000000000, -1000000000);
            long expected = -2000000000;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Substraction_TwoPositiveNumber()
        {
            long actual = summator.SubstractionMoreThenOneNumber(100, new long[] { 10, 6 });
            int expected = 84;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Substraction_NegativeMinuendAndThreeNegativeSubtrahend()
        {
            long actual = summator.SubstractionMoreThenOneNumber(-1, new long[] {-2, -3, -4});
            int expected = 8;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Substraction_EmptyArray()
        {
            long actual = summator.SubstractionMoreThenOneNumber(100, new long[] {});
            int expected = 100;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Substraction_TwoSubtrahend_BigValue()
        {
            long actual = summator.SubstractionMoreThenOneNumber(1, new long[] {3000000000, 3000000000 });
            long expected = -5999999999;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Average_TwoPositiveNumber()
        {
            double actual = summator.Average(new long[] { 5, 6 });
            double expected = 5.5;
            Assert.That(actual == expected);
        }

        [Test]
        public void Average_TwoNegativeNumber()
        {
            double actual = summator.Average(new long[] { -5, -6 });
            double expected = -5.5;
            Assert.That(actual == expected);
        }

        [Test]
        public void Average_BigValue()
        {
            double actual = summator.Average(new long[] {2000000000, 3000000000, 4000000000});            
            Assert.AreEqual(3000000000, actual);
        }

        [Test]
        public void Multiply_TwoPositiveNumbers()
        {
            double actual = summator.Multiply(10, 10);
            Assert.AreEqual(100, actual);
        }

        [Test]
        public void Multiply_TwoNegativeNumbers()
        {
            double actual = summator.Multiply(-10, -10);
            Assert.AreEqual(100, actual);
        }

        [Test]
        public void Multiply_TwoNumbersBigValues()
        {
            double actual = summator.Multiply(3, 300000000);
            Assert.AreEqual(900000000, actual);
        }

        [Test]
        public void Multiply_DoubleNumber()
        {
            double actual = summator.Multiply(1.5, 2);
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Division_TwoPositiveNumbers()
        {
            double actual = summator.Division(15, 3);
            Assert.AreEqual(5, actual);
        }

        [Test]
        public void Division_TwoNegativeNumbers()
        {
            double actual = summator.Division(-15, -3);
            Assert.AreEqual(5, actual);
        }

        [Test]
        public void Division_TwoNumbersBigValues()
        {
            double actual = summator.Division(15000000000, 300000000);
            Assert.AreEqual(50, actual);
        }

        [Test]
        public void Division_DoubleNumber()
        {
            double actual = summator.Division(15.5, 5);
            Assert.AreEqual(3.1, actual);
        }

        [TearDown]
        public void OneTime()
        {
            Console.WriteLine("Test ended.");
        }
    }
}