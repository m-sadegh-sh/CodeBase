namespace CodeBase.Off.Website.AutoMapping.Converters {
    using AutoMapper;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Off.Website.Properties;

    public sealed class NullOrEmptyStringToNotSpecifiedStringConverter : ITypeConverter<string, string> {
        public string Convert(ResolutionContext context) {
            var sourceValue = context.SourceValue.ToStringOrNull();

            if (sourceValue != null)
                return sourceValue;

            return Resources.Texts_NotSpecified;
        }
    }
}