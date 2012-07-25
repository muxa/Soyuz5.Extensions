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
        public void GetFirstDayOfWeek_Monday()
        {
            Assert.AreEqual(new DateTime(2012, 3, 5), new DateTime(2012, 3, 11).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Sunday -> Monday");

            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 12).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Monday == Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 13).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Tuesday -> Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 14).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Wednesday -> Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 15).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Thursday -> Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 16).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Friday -> Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 17).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Saturday -> Monday");
            Assert.AreEqual(new DateTime(2012, 3, 12), new DateTime(2012, 3, 18).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Sunday -> Monday");

            Assert.AreEqual(new DateTime(2012, 3, 19), new DateTime(2012, 3, 19).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Monday == Monday");
            Assert.AreEqual(new DateTime(2012, 3, 19), new DateTime(2012, 3, 20).GetFirstDayOfWeek(DayOfWeek.Monday),
                            "Tuesday -> Monday");
        }

        [Test]
        public void GetFirstDayOfWeek_Sunday()
        {
            Assert.AreEqual(new DateTime(2012, 3, 4), new DateTime(2012, 3, 10).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Saturday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 11).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Sunday == Sunday");

            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 12).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Monday => Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 13).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Tuesday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 14).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Wednesday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 15).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Thursday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 16).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Friday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 11), new DateTime(2012, 3, 17).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Saturday -> Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 18), new DateTime(2012, 3, 18).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Sunday == Sunday");

            Assert.AreEqual(new DateTime(2012, 3, 18), new DateTime(2012, 3, 19).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Monday => Sunday");
            Assert.AreEqual(new DateTime(2012, 3, 18), new DateTime(2012, 3, 20).GetFirstDayOfWeek(DayOfWeek.Sunday),
                            "Tuesday -> Sunday");
        }

        [Test]
        public void GetFirstDayOfWeek_Tuesday()
        {
            Assert.AreEqual(new DateTime(2012, 3, 6), new DateTime(2012, 3, 10).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Saturday -> Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 6), new DateTime(2012, 3, 11).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Sunday => Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 6), new DateTime(2012, 3, 12).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Monday => Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 13).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Tuesday == Tuesday");

            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 14).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Wednesday -> Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 15).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Thursday -> Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 16).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Friday -> Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 17).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Saturday -> Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 18).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Sunday => Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 13), new DateTime(2012, 3, 19).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Monday => Tuesday");
            Assert.AreEqual(new DateTime(2012, 3, 20), new DateTime(2012, 3, 20).GetFirstDayOfWeek(DayOfWeek.Tuesday),
                            "Tuesday == Tuesday");
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

        [Test]
        public void GetFirstDayOfMonth()
        {
            Assert.AreEqual(new DateTime(2011, 12, 1), new DateTime(2011, 12, 31).GetFirstDayOfMonth());
            Assert.AreEqual(new DateTime(2012, 1, 1), new DateTime(2012, 1, 31).GetFirstDayOfMonth());
            Assert.AreEqual(new DateTime(2012, 1, 1), new DateTime(2012, 1, 1).GetFirstDayOfMonth());
            Assert.AreEqual(new DateTime(2012, 2, 1), new DateTime(2012, 2, 1).GetFirstDayOfMonth());
        }

        [Test]
        public void GetLastDayOfMonth()
        {
            Assert.AreEqual(new DateTime(2012, 1, 31), new DateTime(2012, 1, 1).GetLastDayOfMonth());
            Assert.AreEqual(new DateTime(2012, 1, 31), new DateTime(2012, 1, 31).GetLastDayOfMonth());
            Assert.AreEqual(new DateTime(2012, 2, 29), new DateTime(2012, 2, 1).GetLastDayOfMonth());
        }

        [Test]
        public void GetMonthsSpanned()
        {
            Assert.AreEqual(1, new DateTime(2012, 1, 1).GetMonthsSpanned(new DateTime(2012, 1, 1)));
            Assert.AreEqual(1, new DateTime(2012, 1, 1).GetMonthsSpanned(new DateTime(2012, 1, 31)));
            Assert.AreEqual(2, new DateTime(2012, 1, 1).GetMonthsSpanned(new DateTime(2012, 2, 1)));

            // reversed
            Assert.AreEqual(1, new DateTime(2012, 1, 2).GetMonthsSpanned(new DateTime(2012, 1, 1)));
            Assert.AreEqual(2, new DateTime(2012, 1, 1).GetMonthsSpanned(new DateTime(2011, 12, 31)));
            Assert.AreEqual(3, new DateTime(2012, 1, 1).GetMonthsSpanned(new DateTime(2011, 11, 30)));
        }

        [Test]
        public void ParseDateExact_null()
        {
            string s = null;
            Assert.AreEqual(DateTime.MaxValue, s.ParseDateExact("yyyy-MM-dd", DateTime.MaxValue));
        }

        [Test]
        public void ParseDateExact_empty()
        {
            string s = "";
            Assert.AreEqual(DateTime.MaxValue, s.ParseDateExact("yyyy-MM-dd", DateTime.MaxValue));
        }

        [Test]
        public void ParseDateExact_valid_value()
        {
            string s = "2012-01-01";
            Assert.AreEqual(new DateTime(2012, 1, 1), s.ParseDateExact("yyyy-MM-dd", DateTime.MinValue));
        }

        [Test]
        public void ParseDateExact_invalid_value()
        {
            string s = "as 2012-01-01";
            Assert.AreEqual(DateTime.MaxValue, s.ParseDateExact("yyyy-MM-dd", DateTime.MaxValue));
        }

        [Test]
        public void IsSameWeek()
        {
            Assert.AreEqual(false, new DateTime(2012, 07, 19).IsSameWeek(new DateTime(2012, 07, 15, 23, 59, 59)));
            Assert.AreEqual(true, new DateTime(2012, 07, 19).IsSameWeek(new DateTime(2012, 07, 16)));
            Assert.AreEqual(true, new DateTime(2012, 07, 19).IsSameWeek(new DateTime(2012, 07, 22, 23, 59, 59)));
            Assert.AreEqual(false, new DateTime(2012, 07, 19).IsSameWeek(new DateTime(2012, 07, 23)));
        }

        [Test]
        public void IsSameWeek_year_boundary()
        {
            Assert.AreEqual(true, new DateTime(2012, 1, 1).IsSameWeek(new DateTime(2011, 12, 26)));
            Assert.AreEqual(false, new DateTime(2012, 1, 1).IsSameWeek(new DateTime(2012, 1, 2)));
        }

        [Test]
        public void IsSameMonth()
        {
            Assert.AreEqual(false, new DateTime(2012, 07, 1).IsSameMonth(new DateTime(2012, 06, 30, 23, 59, 59)));
            Assert.AreEqual(true, new DateTime(2012, 07, 1).IsSameMonth(new DateTime(2012, 07, 1)));
            Assert.AreEqual(true, new DateTime(2012, 07, 1).IsSameMonth(new DateTime(2012, 07, 31, 23, 59, 59)));
            Assert.AreEqual(false, new DateTime(2012, 07, 1).IsSameMonth(new DateTime(2012, 08, 01)));
        }
    }
}