using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TimeLib;

namespace Tests
{
    public class TimePeriodTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PlusOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(23, 59, 55);
            TimePeriod time2 = new TimePeriod(1, 15, 10);

            var result = time1 + time2;
            Assert.AreEqual(new TimePeriod(25, 15, 5), result);
        }
        [Test]
        public void MinusOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(25, 12, 2);
            TimePeriod time2 = new TimePeriod(1, 15, 10);

            var result = time1 - time2;
            Assert.AreEqual(new TimePeriod(23, 56, 52), result);
        }
        [Test]
        [TestCase(13, 20, 1, "163:20:01")]
        [TestCase(1, 1, 1, "1:01:01")]
        [TestCase(0, 0, 0, "00:00:00")]
        public void ToStringMethodTest(byte hour, byte minute, byte sec, string result)
        {
            TimePeriod time1 = new TimePeriod(hour, minute, sec);
            Assert.AreEqual(result, time1.ToString());
        }
        [Test]
        public void EqualsOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(13, 59, 55);

            var result = time1 == time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void NotEqualsOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(15, 29, 15);

            var result = time1 != time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void EqualsMethodTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(13, 59, 55);

            var result = time1.Equals(time2);
            Assert.IsTrue(result);
        }
        [Test]
        public void IsBiggerOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(15, 29, 15);

            var result = time1 > time2;
            Assert.IsFalse(result);
        }
        [Test]
        public void IsSmallerOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(15, 29, 15);

            var result = time1 < time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void CompareToMethodTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(25, 29, 15);

            var result = time1.CompareTo(time2);
            Assert.IsTrue(result == -1);
        }
        [Test]
        public void IsSmallerOrEqualOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(13, 59, 55);
            TimePeriod time2 = new TimePeriod(125, 29, 15);

            var result = time1 <= time2;
            Assert.IsTrue(result);
        }
        [Test]
        public void IsBiggerOrEqualOperatorTest()
        {
            TimePeriod time1 = new TimePeriod(100, 59, 55);
            TimePeriod time2 = new TimePeriod(125, 29, 15);

            var result = time1 >= time2;
            Assert.IsFalse(result);
        }
    }
}
