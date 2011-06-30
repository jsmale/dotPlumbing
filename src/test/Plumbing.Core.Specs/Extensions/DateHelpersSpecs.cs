using System;
using Machine.Specifications;
using Plumbing.Extensions;

namespace Plumbing.Core.Specs.Extensions
{
    public class DateHelpersSpecs
    {
        public abstract class from_year_month_day
        {
            Because b = () =>
                result = DateHelpers.FromYearMonthDay(year, month, day);

            protected static DateTime? result;
            protected static int year;
            protected static int month;
            protected static int day;
        }

        [Subject(typeof (DateHelpers))]
        public class when_getting_date_from_valid_year_month_day : from_year_month_day
        {
            Establish c = () =>
            {
                year = 2011;
                month = 1;
                day = 1;
            };

            It should_return_correct_date = () => 
                result.ShouldEqual(new DateTime(year, month, day));
        }

        [Subject(typeof (DateHelpers))]
        public class when_getting_date_from_invalid_year_month_day : from_year_month_day
        {
            Establish c = () =>
            {
                year = 2011;
                month = 2;
                day = 30;
            };

            It should_return_null = () =>
                result.ShouldBeNull();
        }

        public abstract class ToMmDdYyyy
        {
            Because b = () =>
                result = date.ToMmDdYyyy();

            protected static DateTime? date;
            protected static string result;
        }

        [Subject(typeof (DateHelpers))]
        public class when_calling_ToMmDdYyyy_on_non_null_datetime : ToMmDdYyyy
        {
            Establish c = () =>
            {
                date = new DateTime(2010, 1, 1);
            };

            It should_return_date_formatted_as_MMDDYYYY = () =>
                result.ShouldEqual(date.Value.ToString("MM/dd/yyyy"));
        }

        [Subject(typeof(DateHelpers))]
        public class when_calling_ToMmDdYyyy_on_null_datetime : ToMmDdYyyy
        {
            Establish c = () =>
            {
                date = null;
            };

            It should_return_empty_string = () =>
                result.ShouldEqual(string.Empty);
        }
    }
}
