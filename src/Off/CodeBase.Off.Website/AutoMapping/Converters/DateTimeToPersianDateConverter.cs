namespace CodeBase.Off.Website.AutoMapping.Converters {
    using System;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Utilities;

    using FarsiLibrary.Utils;

    public sealed class DateTimeToPersianDateConverter : ITypeConverter<DateTime, PersianDate> {
        public PersianDate Convert(ResolutionContext context) {
            var sourceValue = (DateTime) context.SourceValue;

            var dateTimeHelper = IoC.Get<IDateTimeHelper>();

            var userDateTime = dateTimeHelper.ConvertToUserTime(sourceValue);

            return userDateTime;
        }
    }
}