namespace CodeBase.Off.Website.AutoMapping.Converters {
    using AutoMapper;

    using CodeBase.Off.Website.Properties;

    public sealed class BoolToStringConverter : ITypeConverter<bool, string> {
        public string Convert(ResolutionContext context) {
            var sourceValue = (bool) context.SourceValue;

            return sourceValue ? Resources.Texts_True : Resources.Texts_False;
        }
    }
}