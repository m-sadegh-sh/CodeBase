namespace CodeBase.Off.Website.AutoMapping.Converters {
    using System;
    using System.ServiceModel.Syndication;
    using System.Web;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Routing;
    using CodeBase.Off.Core.Domain;

    using MarkdownSharp;

    public sealed class EntryToSyndicationItemConverter : ITypeConverter<Entry, SyndicationItem> {
        public SyndicationItem Convert(ResolutionContext context) {
            var markdown = IoC.Get<Markdown>();
            var request = IoC.Get<HttpRequest>();
            var baseUri = new Uri(request.Url.GetLeftPart(UriPartial.Authority));

            var entry = (Entry) context.SourceValue;

            var syndicationItem = new SyndicationItem(
                entry.Title,
                markdown.Transform(entry.Markdown),
                new Uri(baseUri, "Entry".GetUrl("Show", new {
                    slug = entry.Slug
                })));

            return syndicationItem;
        }
    }
}