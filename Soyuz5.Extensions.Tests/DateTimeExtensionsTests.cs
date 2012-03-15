using System;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void GetMondayOfWeek()
        {
            Assert.AreEqual(new DateTime(2011, 1, 10), new DateTime(2011, 1, 13).GetMondayOfWeek());
            Assert.AreEqual(new DateTime(2011, 1, 10), new DateTime(2011, 1, 10).GetMondayOfWeek());
            Assert.AreEqual(new DateTime(2011, 1, 10), new DateTime(2011, 1, 16).GetMondayOfWeek());
            Assert.AreEqual(new DateTime(2011, 1, 17), new DateTime(2011, 1, 17).GetMondayOfWeek());
        }

        [Test]
        public void GetFirstMondayOfYearTest()
        {
            Assert.AreEqual(new DateTime(2006, 1, 2), new DateTime(2006, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2007, 1, 1), new DateTime(2007, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2008, 1, 7), new DateTime(2008, 1, 1).GetFirstMondayOfYear());
                //but week 1 starts on 31/12/2007
            Assert.AreEqual(new DateTime(2009, 1, 5), new DateTime(2009, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2010, 1, 4), new DateTime(2010, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2011, 1, 3), new DateTime(2011, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2012, 1, 2), new DateTime(2012, 1, 1).GetFirstMondayOfYear());
            Assert.AreEqual(new DateTime(2013, 1, 7), new DateTime(2013, 1, 1).GetFirstMondayOfYear());
                //but week 1 starts on 31/12/2012
        }

        [Test]
        public void GetWeekNumber()
        {
            Assert.AreEqual(27, new DateTime(2011, 07, 06).GetWeekNumber());
        }
    }
}