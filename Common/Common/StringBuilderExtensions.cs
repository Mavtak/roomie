using System;
using System.Text;

namespace Roomie.Common
{
    public static class StringBuilderExtensions
    {
        public static void AppendDateTime(this StringBuilder builder, DateTime? dateTime, TimeZoneInfo timeZone = null)
        {
            if (dateTime != null && timeZone != null)
            {
                dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, timeZone);
            }

            builder.Append(dateTime);
        }
    }
}
