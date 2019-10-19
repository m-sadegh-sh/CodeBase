namespace CodeBase.Common.Infrastructure.Application {
    using System;

    using FarsiLibrary.Utils;

    public static class DateExtensions {
        public static string ToPrettyDate(this DateTime value) {
            var timeSince = DateTime.Now.Subtract(value);

            if (timeSince.TotalMilliseconds < 1)
                return "نه هنوز";

            if (timeSince.TotalMinutes < 1)
                return "همین الان";

            if (timeSince.TotalMinutes < 60)
                return string.Format("{0} ثانیه پیش", timeSince.Minutes);

            if (timeSince.TotalHours < 24)
                return string.Format("{0} ساعت پیش", timeSince.Hours);

            if (timeSince.TotalDays == 1)
                return "دیروز";

            if (timeSince.TotalDays < 7)
                return string.Format("{0} روز قبل", timeSince.Days);

            if (timeSince.TotalDays < 14)
                return "هفته گذشته";

            if (timeSince.TotalDays < 21)
                return "دو هفته پیش";

            if (timeSince.TotalDays < 28)
                return "سه هفته پیش";

            if (timeSince.TotalDays < 60)
                return "ماه گذشته";

            if (timeSince.TotalDays < 365)
                return string.Format("{0} ماه پیش", Math.Round(timeSince.TotalDays/30));

            if (timeSince.TotalDays < 730)
                return "سال گذشته";

            return string.Format("{0} سال قبل", Math.Round(timeSince.TotalDays/365));
        }

        public static string ToDateString(this DateTime value) {
            return value.ToString("dddd, dd MMMM yyyy");
        }

        public static int Timestamp(this DateTime value) {
            var timestamp = (int) (value - new DateTime(1970, 1, 1)).TotalSeconds;

            return timestamp;
        }

        public static string ToPersianDateString(this DateTime dateTime, bool toWritten = true) {
            return toWritten ? PersianDateConverter.ToPersianDate(dateTime).ToWritten() : PersianDateConverter.ToPersianDate(dateTime).ToString();
        }

        public static PersianDate ToPersianDate(this DateTime dateTime) {
            return PersianDateConverter.ToPersianDate(dateTime);
        }

        public static DateTime ToGeorgianDate(this PersianDate dateTime) {
            return PersianDateConverter.ToGregorianDateTime(dateTime);
        }
    }
}