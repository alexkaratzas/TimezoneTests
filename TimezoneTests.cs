using System;
using System.Diagnostics;
using NodaTime;
using Xunit;

namespace Date.Tests
{
    public class TimezoneTests
    {
        [Fact]
        public void Convert_Now_ToLocal()
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();

            DateTimeZone tz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/London");
            //DateTimeZone tz = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            DateTimeZone bcl = DateTimeZoneProviders.Bcl.GetSystemDefault();

            ZonedDateTime zdt = now.InZone(tz);

            Debug.WriteLine(now);
            Debug.WriteLine(tz);
            Debug.WriteLine(zdt);
        }

        [Fact]
        public void DSTSpring()
        {
            DateTimeZone london = DateTimeZoneProviders.Tzdb["Europe/London"];
            // 12:45am on March 25th 2012
            LocalDateTime local = new LocalDateTime(2012, 3, 25, 0, 45, 00);
            ZonedDateTime before = london.AtStrictly(local);
            ZonedDateTime after = before + Duration.FromMinutes(20);


            Debug.WriteLine(london);
            Debug.WriteLine(before);
            Debug.WriteLine(after);
        }

        [Fact]
        public void DSTAutumn()
        {
            DateTimeZone london = DateTimeZoneProviders.Tzdb["Europe/London"];
            // 12:45am on October 29th 2017
            LocalDateTime local = new LocalDateTime(2017, 10, 29, 0, 50, 00);
            ZonedDateTime before = london.AtStrictly(local) + Duration.FromMinutes(10);
            ZonedDateTime after = before + Duration.FromMinutes(60);


            Debug.WriteLine(london);
            Debug.WriteLine(before);
            Debug.WriteLine(after);
        }

        [Fact]
        public void UnixTime_Older_Than_1970()
        {
            var date = new DateTimeOffset(1950, 1, 1, 0, 1, 0, TimeSpan.Zero);

            var unixTime = date.ToUnixTimeSeconds();

            Debug.WriteLine(unixTime);
        }

        [Fact]
        public void UnixTime_RelationToUtc()
        {


            Instant now = SystemClock.Instance.GetCurrentInstant();

            DateTimeZone tz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/London");
            //DateTimeZone tz = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            DateTimeZone bcl = DateTimeZoneProviders.Bcl.GetSystemDefault();

            ZonedDateTime zdt = now.InZone(tz);

            var date = new OffsetDateTime(zdt.LocalDateTime, zdt.Offset); //1950, 1, 1, 0, 1, 0, TimeSpan.Zero);

            //Convert to UNIX
            var unixTime = zdt.ToInstant().ToUnixTimeSeconds();



            Debug.WriteLine(unixTime);
        }
    }
}
