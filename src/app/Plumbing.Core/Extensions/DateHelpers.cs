using System;

namespace Plumbing.Extensions
{
    public static class DateHelpers
    {
        public static DateTime? FromYearMonthDay(int year, int month, int day)
        {
            if (month > 12 || month < 1 || day > 31 || day < 1) return null;

            try
            {
                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }

        public static string ToLocalString(this DateTime? value, string format = null)
        {
            if (value == null) return "";
            var date = value.Value.ToLocalTime();
            if (format != null)
            {
                return date.ToString(format);
            }
            return date.ToString();
        }

        public static string ToMmDdYyyy(this DateTime? value)
        {
            if (value == null) return "";
            return value.Value.ToString("MM/dd/yyyy");
        }
    }
}