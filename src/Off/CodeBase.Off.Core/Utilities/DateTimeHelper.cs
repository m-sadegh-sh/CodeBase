namespace CodeBase.Off.Core.Utilities {
    using System;
    using System.Collections.ObjectModel;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Services.Impl;

    using FarsiLibrary.Utils;

    public sealed class DateTimeHelper : IDateTimeHelper {
        private readonly IAttributeService<string> _userAttributeService;
        private readonly User _currentUser;
        private readonly IConfigService _configService;
        private readonly Config _config;

        public DateTimeHelper(IAttributeService<string> userAttributeService, User currentUser, IConfigService configService, Config config) {
            _userAttributeService = userAttributeService;
            _currentUser = currentUser;
            _configService = configService;
            _config = config;
        }

        public TimeZoneInfo DefaultTimeZone {
            get {
                var timeZoneInfo = TimeZoneInfo.Local;

                if (!string.IsNullOrEmpty(_config.DefaultTimeZoneId) && _config.DefaultTimeZoneId.IsValidTimeZoneId())
                    timeZoneInfo = FindTimeZoneById(_config.DefaultTimeZoneId);

                if (_config.DefaultTimeZoneId != timeZoneInfo.Id)
                    DefaultTimeZone = timeZoneInfo;

                return timeZoneInfo;
            }
            set {
                var defaultTimeZoneId = string.Empty;

                if (value != null)
                    defaultTimeZoneId = value.Id;

                _config.DefaultTimeZoneId = defaultTimeZoneId;
                _configService.Save();
            }
        }

        public TimeZoneInfo CurrentTimeZone {
            get { return GetUserTimeZone(_currentUser); }
            set {
                if (!_config.AllowUsersToSetTimeZone)
                    return;

                var timeZoneId = string.Empty;

                if (value != null)
                    timeZoneId = value.Id;

                if (_currentUser == null)
                    return;

                _userAttributeService.Save(_currentUser.UserName, User.Attributes.TimeZoneId, timeZoneId);
            }
        }

        public TimeZoneInfo FindTimeZoneById(string id) {
            return TimeZoneInfo.FindSystemTimeZoneById(id);
        }

        public ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones() {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        public DateTime ConvertToUserTime(DateTime dateTime) {
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            var currentUserTimeZoneInfo = CurrentTimeZone;

            return TimeZoneInfo.ConvertTime(dateTime, currentUserTimeZoneInfo);
        }

        public DateTime ConvertToUserTime(DateTime dateTime, TimeZoneInfo sourceTimeZone) {
            var currentUserTimeZoneInfo = CurrentTimeZone;

            return ConvertToUserTime(dateTime, sourceTimeZone, currentUserTimeZoneInfo);
        }

        public DateTime ConvertToUserTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone) {
            return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
        }

        public DateTime ConvertToUtcTime(DateTime dateTime) {
            return ConvertToUtcTime(dateTime, dateTime.Kind);
        }

        public DateTime ConvertToUtcTime(DateTime dateTime, DateTimeKind sourceDateTimeKind) {
            dateTime = DateTime.SpecifyKind(dateTime, sourceDateTimeKind);

            return TimeZoneInfo.ConvertTimeToUtc(dateTime);
        }

        public DateTime ConvertToUtcTime(DateTime dateTime, TimeZoneInfo sourceTimeZone) {
            if (sourceTimeZone.IsInvalidTime(dateTime))
                return dateTime;

            return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);
        }

        public TimeZoneInfo GetUserTimeZone(User user) {
            TimeZoneInfo timeZoneInfo = null;

            if (_config.AllowUsersToSetTimeZone) {
                var timeZoneId = string.Empty;
                if (user != null)
                    timeZoneId = _userAttributeService.TryGetValue(user.UserName, User.Attributes.TimeZoneId, "");

                if (timeZoneId.IsValidTimeZoneId())
                    timeZoneInfo = FindTimeZoneById(timeZoneId);
            }

            return timeZoneInfo ?? DefaultTimeZone;
        }

        public PersianDate TranslateToPersian(DateTime dateTime) {
            return dateTime.ToPersianDate();
        }

        public DateTime TranslateToGregorian(PersianDate persianDate) {
            return persianDate.ToGeorgianDate();
        }
    }
}