namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using AutoMapper;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Website.Properties;

    public sealed class LogSeverityLevelToStringResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var level = (Log.SeverityLevel) source.Value;

            var @string = level.ToString();

            switch (level) {
                case Log.SeverityLevel.Debug:
                    @string = Resources.Texts_SeverityLevel_Debug;
                    break;
                case Log.SeverityLevel.Info:
                    @string = Resources.Texts_SeverityLevel_Info;
                    break;
                case Log.SeverityLevel.Warning:
                    @string = Resources.Texts_SeverityLevel_Warning;
                    break;
                case Log.SeverityLevel.Error:
                    @string = Resources.Texts_SeverityLevel_Error;
                    break;
                case Log.SeverityLevel.Crud:
                    @string = Resources.Texts_SeverityLevel_Crud;
                    break;
            }

            return source.New(@string);
        }
    }
}