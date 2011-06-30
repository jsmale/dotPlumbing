using System;
using FileHelpers;
using Plumbing.Extensions;

namespace Plumbing.Files
{
    namespace FileHelperConverters
    {
        public class YyyyMmDdWithDefaultMinValue : YyyyMmDd
        {
            public override object StringToField(string from)
            {
                return base.StringToField(from) ?? DateTime.MinValue;
            }
        }

        public class YyyyMmDd : ConverterBase
        {
            public override string FieldToString(object from)
            {
                if (from == null)
                {
                    return "        ";
                }

                if (!(from is DateTime?))
                {
                    throw new ArgumentException("Must be a DateTime?", "from");
                }

                var date = (DateTime)from;
                return date.ToString("yyyyMMdd");
            }

            public override object StringToField(string from)
            {
                return ConvertFrom(from);
            }

            public static DateTime? ConvertFrom(string from)
            {
                if (String.IsNullOrEmpty(from) || !from.Is8DigitInteger()) return null;

                var year = Convert.ToInt32(from.Substring(0, 4));
                var month = Convert.ToInt32(from.Substring(4, 2));
                var day = Convert.ToInt32(from.Substring(6, 2));
                return DateHelpers.FromYearMonthDay(year, month, day);
            }
        }

        public class MmDdYyyy : ConverterBase
        {
            public override object StringToField(string from)
            {
                if (String.IsNullOrEmpty(from) || !from.Is8DigitInteger()) return null;

                var month = Convert.ToInt32(from.Substring(0, 2));
                var day = Convert.ToInt32(from.Substring(2, 2));
                var year = Convert.ToInt32(from.Substring(4, 4));
                return DateHelpers.FromYearMonthDay(year, month, day);
            }
        }

        public class DateString : ConverterBase
        {
            public override object StringToField(string from)
            {
                DateTime result;
                if (DateTime.TryParse(from, out result)) return result;

                return null;
            }
        }

        public class Int64Converter : ConverterBase
        {
            public override object StringToField(string from)
            {
                Int64 result;
                if (Int64.TryParse(from, out result)) return result;

                return null;
            }
        }

        public class TwoDecimalConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                if (String.IsNullOrEmpty(from) || !from.IsInteger()) return 0;

                var res = Convert.ToDouble(from);
                return res / 100.0;
            }

            public override string FieldToString(object from)
            {
                var value = from as double?;
                if (value == null) return string.Empty;

                return Convert.ToInt64(value * 100).ToString();
            }
        }

        public class BoolConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                if (from.IsNullOrEmpty())
                {
                    return false;
                }

                var value = from.Trim().ToLowerInvariant();
                return (value == "t" || value == "true" || value == "y" || value == "yes" || value == "1");
            }
        }

        public class ThreeDecimalConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                if (from.IsNullOrEmpty() || !from.IsInteger()) return 0;

                var res = Convert.ToDouble(from);
                return res / 1000.0;
            }

            public override string FieldToString(object from)
            {
                var value = from as double?;
                return value != null
                    ? Convert.ToInt64(value*1000).ToString()
                    : string.Empty;
            }
        }
    }
}