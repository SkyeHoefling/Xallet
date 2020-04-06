using System;

namespace Xallet.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimeToDateTimeUtc(long unixtime)
        {
            var datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return datetime.AddSeconds(unixtime);
        }
    }
}
