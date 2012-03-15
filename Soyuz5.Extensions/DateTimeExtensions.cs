using System;

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
        /// Gets first date of the month that the date. Discards time component.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return date.Date.AddDays(1 - date.Day);
        }
    }
}