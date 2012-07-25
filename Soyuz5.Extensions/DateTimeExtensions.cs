using System;
using System.Globalization;

namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Rounds time with seconds precision (removing milliseonds and ticks)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime RoundToSecond(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// If date is no null rounds time with seconds precision (removing milliseonds and ticks). 
        /// Otherwsie returns null
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime? RoundToSecond(this DateTime? dt)
        {
            if (dt.HasValue)
                return new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second);

            return null;
        }

        /// <summary>
        /// Rounds time with minutes precision (removing seconds, milliseonds and ticks)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime RoundToMinute(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }

        /// <summary>
        /// Gets Monday of the week that the date falls on. Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetMondayOfWeek(this DateTime date)
        {
            //convert from sun=0 & sat=6 to mon=0 & sun=6
            int dayOfTheWeek = (int)date.DayOfWeek;
            if (dayOfTheWeek == 0)
                dayOfTheWeek = 7;
            dayOfTheWeek--; //here we have mon=0 & sun=6
            return date.Date.AddDays(-dayOfTheWeek);
        }

        /// <summary>
        /// Gets day of the week that the date falls on (using current culture). Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            return GetFirstDayOfWeek(date, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Gets day of the week that the date falls on (using specified culture). Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return GetFirstDayOfWeek(date, cultureInfo.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Gets first day of the week that the date falls on (or before). Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="firstDay"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, DayOfWeek firstDay)
        {
            // taken from first comment on http://joelabrahamsson.com/entry/getting-the-first-date-in-a-week-with-c-sharp

            /* first day of week:   sun    mon   thu
             *                           diff
             * date day:    sun      0      6     5
             *              mon      1      0     6
             *              tues     2      1     0
             *              wed      3      2     1
             *              thu      4      3     2
             *              fri      5      4     3
             *              sat      6      5     4
             */
            

            int difference = ((int)date.DayOfWeek) - ((int)firstDay);
            difference = (7 + difference) % 7;
            return date.Date.AddDays(-difference).Date;
        }

        /// <summary>
        /// Gets first date of the month that the date. Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return date.Date.AddDays(1 - date.Day);
        }

        /// <summary>
        /// Gets last date of the month that the date. Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Gets number of calendar months that from and to dates touch. E.g. 31/01/2012 - 1/03/2012 touch 3 months, 1/02/2012 - 1/03/2012 touch 2 months, 1/02/2012 - 28/02/2012 touch 1 month
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"> </param>
        /// <returns></returns>
        public static int GetMonthsSpanned(this DateTime fromDate, DateTime toDate)
        {
            if (toDate > fromDate)
            {
                return (toDate.Month - fromDate.Month) + (toDate.Year - fromDate.Year)*12 + 1;
            }
            else
            {
                return (fromDate.Month - toDate.Month) + (fromDate.Year - toDate.Year)*12 + 1;
            }
        }

        /// <summary>
        /// Checks if the two dates fall on the same week.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"> </param>
        /// <returns></returns>
        public static bool IsSameWeek(this DateTime date1, DateTime date2)
        {
            DateTime weekStart = date1.GetFirstDayOfWeek();
            if (date2 < weekStart)
                return false;

            if (date2 >= weekStart.AddDays(7))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the two dates fall on the same month.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"> </param>
        /// <returns></returns>
        public static bool IsSameMonth(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month;
        }

        /// <summary>
        /// Checks if the two dates fall on the same year.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"> </param>
        /// <returns></returns>
        public static bool IsSameYear(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year;
        }

        /// <summary>
        /// Returns true if the <paramref name="date"/> falls on a weekend.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns true if the <paramref name="date"/> does not fall on a weekend.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWorkday(this DateTime date)
        {
            return !IsWeekend(date);
        }

        /// <summary>
        /// Gets week number the year fall on (US version, where week 1 begins on the first Monday of the Year)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekNumber(this DateTime date)
        {
            DateTime firstMonday = date.GetFirstMondayOfYear();

            return 1 + (date.DayOfYear - firstMonday.DayOfYear) / 7;
        }

        /// <summary>
        /// Gets first monday of the year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetFirstMondayOfYear(this DateTime year)
        {
            DateTime firstDay = new DateTime(year.Year, 1, 1);

            if (firstDay.DayOfWeek == DayOfWeek.Monday)
                return firstDay;

            if (firstDay.DayOfWeek == DayOfWeek.Sunday)
                return firstDay.AddDays(1);

            firstDay = firstDay.AddDays(8 - (int)firstDay.DayOfWeek);

            //Debug.Assert(firstDay.DayOfWeek == DayOfWeek.Monday);

            return firstDay;
        }

        /// <summary>
        /// Tries to parse input string as DateTime of specified format. If unsuccessful returns default value.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="defaultValue"></param>
        /// <param name="dateTimeStyles"></param>
        /// <returns></returns>
        public static DateTime ParseDateExact(this string s, string format, DateTime defaultValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            DateTime value;
            if (DateTime.TryParseExact(s, format, null, dateTimeStyles, out value))
                return value;

            return defaultValue;
        }
    }
}