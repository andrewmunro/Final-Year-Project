﻿using System;

using MediBook.Shared.Config;

namespace MediBook.Shared.utils
{
    public static class DateTimeExtensions
    {
        static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixEpoch(this int utc_unix)
        {
            return epoch.AddSeconds(utc_unix);
        }

        public static DateTime FromUnixEpoch(this long utc_unix)
        {
            return epoch.AddSeconds(utc_unix);
        }

        public static long ToUnixEpoch(this DateTime dt)
        {
            dt = dt.ToUniversalTime();
            return (long)((dt - epoch).TotalMilliseconds);
        }

        public static DateTime ToGeographicalLocal(this DateTime dt)
        {
            var tZone = TimeZoneInfo.FindSystemTimeZoneById(Configuration.TimeZone);

            var adjustedTime = TimeZoneInfo.ConvertTimeFromUtc(dt, tZone);

            //TODO Find better solution
            //Hack because TimeZoneInfo.ConvertTimeFromUtc is broken in mono.
            if (dt.Hour == adjustedTime.Hour) return adjustedTime.AddHours(1);

            return adjustedTime;
        }

        public static String ToFormattedString(this DateTime dt)
        {
            var ldt = dt.ToGeographicalLocal();
            return ldt.ToString("ddd, MMM d, yyyy") + " at " + ldt.ToString("HH:mm tt");
        }
    }
}
