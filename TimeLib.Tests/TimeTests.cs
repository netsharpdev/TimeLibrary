using System;
using NUnit.Framework;
using TimeLib;

namespace Tests
{
    public class TimeTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase("13:20:01", 13, 20, 1)]
        public void FromStringConstructorTest(string timeVal, byte resultHours, byte resultMinutes, byte resultSeconds)
        {
            var time = new Time(timeVal);
            Assert.AreEqual(time.Hours, resultHours);
            Assert.AreEqual(time.Minutes, resultMinutes);
            Assert.AreEqual(time.Seconds, resultSeconds);
        }
        [Test]
        public void PlusOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(1, 15, 10);

            var result = time1 + time2;
            Assert.AreEqual(new Time(15, 15, 5), result);
        }
        [Test]
        public void MinusOperatorTest()
        {
            Time time1 = new Time(13, 12, 2);
            Time time2 = new Time(1, 15, 10);

            var result = time1 - time2;
            Assert.AreEqual(new Time(11, 56, 52), result);
        }
        [Test]
        [TestCase(13,20,1, "13:20:01")]
        [TestCase(1, 1, 1, "01:01:01")]
        [TestCase(0, 0, 0, "00:00:00")]
        public void ToStringMethodTest(byte hour, byte minute, byte sec, string result)
        {
            Time time1 = new Time(hour, minute, sec);
            Assert.AreEqual(result, time1.ToString());
        }
        [Test]
        public void EqualsOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(13,59, 55);

            var result = time1 == time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void NotEqualsOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1 != time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void EqualsMethodTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(13, 59, 55);

            var result = time1.Equals(time2);
            Assert.IsTrue(result);
        }
        [Test]
        public void IsBiggerOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1 > time2;
            Assert.IsFalse(result);
        }
        [Test]
        public void IsSmallerOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1 < time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void CompareToMethodTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1.CompareTo(time2);
            Assert.IsTrue(result == -1);
        }
        [Test]
        public void IsSmallerOrEqualOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1 <= time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void IsBiggerOrEqualOperatorTest()
        {
            Time time1 = new Time(13, 59, 55);
            Time time2 = new Time(15, 29, 15);

            var result = time1 >= time2;
            Assert.IsFalse(result);
        }
        [Test]
        public void AddTimePeriodTest()
        {
            Time time1 = new Time(13, 59, 55);
            TimePeriod period = new TimePeriod(1, 15, 10);

            var result = time1.Add(period);
            Assert.AreEqual(new Time(15, 15, 5), result);
        }
        [Test]
        public void SubstractTimePeriodTest()
        {
            Time time1 = new Time(13, 10, 5);
            TimePeriod period = new TimePeriod(10, 15, 10);
            var result = time1.Substract(period);
            Assert.AreEqual(new Time(2,54,55), result);
        }
    }
}