using System;

namespace NCStudio.Utility.Extensions
{
    public static class DateTimeExtension
    {
        public static double DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            return (dateTime -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
