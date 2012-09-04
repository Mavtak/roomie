using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Roomie.Desktop.Engine
{
    /// <summary>
    /// This class provides utility methods for converting TimeSpans and DateTimes to and from strings.
    /// 
    /// This code was written by David McGrath. (http://davidmcgrath.com)
    /// Reduce, reuse, recycle!
    /// </summary>
    public static class TimeUtils
    {
        #region Time Spans
        /// <summary>
        /// This regular expression matches strings that represent time spans.
        /// </summary>
        private const string timeSpanPattern = @"^"//maybe each of the following groups
        + @"(?: (?<days>         \d+) \s* (?: days|day|d)                  )?\s*" //one or more digits, maybe whitespace, days or day or d.  Maybe whitespace after
        + @"(?: (?<hours>        \d+) \s* (?: hours|hour|h)                )?\s*"
        + @"(?: (?<minutes>      \d+) \s* (?: minutes|minute|m)            )?\s*"
        + @"(?: (?<seconds>      \d+) \s* (?: seconds|second|s)            )?\s*"
        + @"(?: (?<milliseconds> \d+) \s* (?: milliseconds|millisecond|ms) )?"
        + @"$";
        private static readonly Regex timeSpanRegex = new Regex(timeSpanPattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Returns true if the given string represents a valid timespan.  Valid time spans include '1 day', '3 hours 2 minutes', '1s3ms', and '1 day3ms'.  Invalid time spans include '2 minutes 3 hours'.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if the input is a valid timespan, otherwise false.</returns>
        public static bool IsTimeSpan(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            if (String.IsNullOrEmpty(input.Trim()))
                return false;

            return timeSpanRegex.IsMatch(input);
        }

        /// <summary>
        /// Converts the input string to a TimeSpan.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TimeSpan StringToTimeSpan(string input)
        {
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int milliseconds = 0;

            Match match = timeSpanRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid TimeSpan", "input");
            }

            if (match.Groups["days"].Success)
                days = Convert.ToInt32(match.Groups["days"].Value);
            if (match.Groups["hours"].Success)
                hours = Convert.ToInt32(match.Groups["hours"].Value);
            if (match.Groups["minutes"].Success)
                minutes = Convert.ToInt32(match.Groups["minutes"].Value);
            if (match.Groups["seconds"].Success)
                seconds = Convert.ToInt32(match.Groups["seconds"].Value);
            if (match.Groups["milliseconds"].Success)
                milliseconds = Convert.ToInt32(match.Groups["milliseconds"].Value);

            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Converts a TimeSpan to string representation that can be interpreted by this class.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TimeSpanToString(TimeSpan input)
        {
            StringBuilder result = new StringBuilder();

            if (input.Days > 0)
                result.Append(input.Days + "d");
            if (input.Hours > 0)
                result.Append(input.Hours + "h");
            if (input.Minutes > 0)
                result.Append(input.Minutes + "m");
            if (input.Seconds > 0)
                result.Append(input.Seconds + "s");
            if (input.Milliseconds > 0)
                result.Append(input.Milliseconds + "ms");

            return result.ToString();
        }

        #endregion

        #region DateTime

        /// <summary>
        /// This regular expression matches strings that represent dates and times.
        /// </summary>
        private const string timePattern = @"^"

            //date optional
            + @"(?<date> "
            + @"(?:   (?<year>  \d{4}   )  )?"
            + @"(?: / (?<month> \d{1,2} )  )"
            + @"(?: / (?<day>   \d{1,2} )  )"
            + @")?"//close date group

            + @"\s*"

            //time optional
            + @"(?<time>"
            + @"      (?<hour>   \d+)"
            + @"(?: : (?<minute> \d+)  )?"
            + @"(?: [:.] (?<second> \d+)  )?"
            + @"(?<AmPm> (?: am|pm)   )?"
            + @")"//close time group
            + @"$";
        private static Regex timePatternRegex = new Regex(timePattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Returns true if the given input represents a valid date and time.
        /// The date portion is optional, as is the year itself.
        /// Times without dates will have dates autoselected to be nearest in the future (today or tomorrow, depending).
        /// 
        /// Valid dates and times include '1:12am', '12/20', '2010/12/20 1pm', '1999/1/02 13:12.59', and '10:11:12'.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDateTime(string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            if (String.IsNullOrEmpty(input.Trim()))
                return false;

            return timePatternRegex.IsMatch(input);
        }

        /// <summary>
        /// Converts the given string to a TimeSpan
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string input)
        {
            DateTime now = DateTime.Now;

            int year = 0;
            int month = 0;
            int day = 0;

            int hour = 0;
            int minute = 0;
            int second = 0;

            Match match = timePatternRegex.Match(input);

            if (!match.Success)
            {
                throw new ArgumentException("The input does not represent a valid DateTime", "input");
            }

            if (match.Groups["date"].Success)
            {
                if (match.Groups["year"].Success)
                    year = Convert.ToInt32(match.Groups["year"].Value);
                else
                    year = now.Year;

                month = Convert.ToInt32(match.Groups["month"].Value);
                day = Convert.ToInt32(match.Groups["day"].Value);
            }
            else
            {
                year = now.Year;
                month = now.Month;
                day = now.Day;
            }

            if (match.Groups["hour"].Success)
                hour = Convert.ToInt32(match.Groups["hour"].Value);
            if (match.Groups["minute"].Success)
                minute = Convert.ToInt32(match.Groups["minute"].Value);
            if (match.Groups["second"].Success)
                second = Convert.ToInt32(match.Groups["second"].Value);

            if (match.Groups["AmPm"].Success && match.Groups["AmPm"].Value.ToLower().Replace(".", "").Equals("pm"))
            {
                if (hour > 12)
                    throw new ArgumentOutOfRangeException("invalid hour");
                hour = (hour + 12) % 24;
            }

            if (hour > 23)
                throw new ArgumentOutOfRangeException("invalid hour");
            if (minute > 59)
                throw new ArgumentOutOfRangeException("invalid minute");
            if (second > 59)
                throw new ArgumentOutOfRangeException("invalid second");

            DateTime result = new DateTime(year, month, day, hour, minute, second);

            if (result < now)
            {
                if (!match.Groups["date"].Success)
                    result = result.AddDays(1);
                else if (!match.Groups["year"].Success)
                    result = result.AddYears(1);
            }

            return result;

        }

        /// <summary>
        /// Converts the DateTime to a string representation that can be interpreted by this class.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime input)
        {
            StringBuilder result = new StringBuilder();

            result.Append(input.Year);
            result.Append("/");
            result.Append(input.Month);
            result.Append("/");
            result.Append(input.Day);
            result.Append(" ");
            result.Append(input.Hour);
            result.Append(":");
            result.Append(input.Minute);
            result.Append(".");
            result.Append(input.Second);

            return result.ToString();
        }
        #endregion
    }
}
