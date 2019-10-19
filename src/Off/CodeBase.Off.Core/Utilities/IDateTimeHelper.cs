namespace CodeBase.Off.Core.Utilities {
    using System;
    using System.Collections.ObjectModel;

    using CodeBase.Off.Core.Domain;

    using FarsiLibrary.Utils;

    public interface IDateTimeHelper {
        TimeZoneInfo DefaultTimeZone { get; set; }
        TimeZoneInfo CurrentTimeZone { get; set; }
        TimeZoneInfo FindTimeZoneById(string id);
        ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones();
        DateTime ConvertToUserTime(DateTime dateTime);
        DateTime ConvertToUserTime(DateTime dateTime, TimeZoneInfo sourceTimeZone);
        DateTime ConvertToUserTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone);
        DateTime ConvertToUtcTime(DateTime dateTime);
        DateTime ConvertToUtcTime(DateTime dateTime, DateTimeKind sourceDateTimeKind);
        DateTime ConvertToUtcTime(DateTime dateTime, TimeZoneInfo sourceTimeZone);
        TimeZoneInfo GetUserTimeZone(User user);
        PersianDate TranslateToPersian(DateTime dateTime);
        DateTime TranslateToGregorian(PersianDate persianDate);
    }
}