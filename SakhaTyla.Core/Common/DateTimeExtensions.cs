using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace SakhaTyla.Core.Common
{
    public static class DateTimeExtensions
    {
        private static readonly TimeZoneInfo _applicationTimeZone = TZConvert.GetTimeZoneInfo("Asia/Yakutsk");

        public static DateTime? ToApplicationTime(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            return dateTime.Value.ToApplicationTime();
        }

        public static DateTime ToApplicationTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, _applicationTimeZone);
        }

        public static DateTime? FromApplicationTime(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            return dateTime.Value.FromApplicationTime();
        }

        public static DateTime FromApplicationTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, _applicationTimeZone);
        }
    }
}
