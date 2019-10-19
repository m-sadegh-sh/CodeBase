namespace CodeBase.Off.Website.AutoMapping.Converters {
    using System;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Utilities;

    public sealed class DateTimeToStringConverter : ITypeConverter<DateTime, string> {
        private const string PRETTY_PRESENTATIO_KEY = "Pretty";

        public string Convert(ResolutionContext context) {
            var sourceValue = (DateTime) context.SourceValue;

            var dateTimeHelper = IoC.Get<IDateTimeHelper>();

            var userDateTime = dateTimeHelper.ConvertToUserTime(sourceValue);

            var toPretty = context.MemberName.Contains(PRETTY_PRESENTATIO_KEY);

            if (toPretty)
                return userDateTime.ToPrettyDate();

            return userDateTime.ToDateString();
        }
    }
}