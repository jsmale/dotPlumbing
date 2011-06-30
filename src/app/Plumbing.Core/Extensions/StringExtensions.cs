using System;
using System.Globalization;
using System.Text;

namespace Plumbing.Extensions
{
    public static class StringExtensions
    {
        static Encoding utf8 = Encoding.UTF8;

        public static byte[] ToBytes(this string value)
        {
            return utf8.GetBytes(value);
        }

        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string ToBase64(this string value)
        {
            return value.ToBytes().ToBase64();
        }

        public static string DecodeToString(this byte[] bytes)
        {
            return utf8.GetString(bytes);
        }

        public static byte[] GetBytesFromBase64(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static string GetStringFromBase64(this string base64String)
        {
            return base64String.GetBytesFromBase64().DecodeToString();
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        public static string ToFormat(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static string ToProperCase(this string value)
        {
            if (value == null) return null;
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(textInfo.ToLower(value));
        }

        public static Int32? ToInt32(this string value)
        {
            Int32 result;
            if (Int32.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static Int64? ToInt64(this string value)
        {
            Int64 result;
            if (Int64.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static Double? ToDouble(this string value)
        {
            Double result;
            if (Double.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static DateTime? ToDateTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }
    }
}