namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using AutoMapper;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;

    using MarkdownSharp;

    public sealed class MarkdownToHtmlResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var value = source.Value.ToStringOrNull();

            if (value == null)
                return source.New(string.Empty);

            var markdown = IoC.Get<Markdown>();

            var html = markdown.Transform(value);

            return source.New(html);
        }
    }
}