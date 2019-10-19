namespace CodeBase.Common.Infrastructure.Application {
    using System;

    public static class TimeZoneInfoExtensions {
        public static bool IsValidTimeZoneId(this string timeZoneId) {
            try {
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

                return true;
            } catch {
                return false;
            }
        }
    }
}