using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool TryParseToDateTime(this string str, string format, out DateTime date)
        {
            return DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date);
        }

        public static DateTime? ParseToNullableDateTime(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : DateTime.Parse(str);
        }

        public static DateTime FirstDateOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDateOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime NextDateByDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            do
            {
                dateTime = dateTime.AddDays(1);
            }
            while (dateTime.DayOfWeek != dayOfWeek);
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static bool IsSameMonth(this DateTime dateTime, DateTime dateToCompare)
        {
            return dateTime.Month == dateToCompare.Month && dateTime.Year == dateToCompare.Year;
        }

        public static bool IsInWeekendDays(this DateTime dateTime)
        {
            DayOfWeek[] weekendDays = new DayOfWeek[]
            {
                 DayOfWeek.Saturday,
                 DayOfWeek.Sunday
            };

            return weekendDays.Contains(dateTime.DayOfWeek);
        }
    }
}