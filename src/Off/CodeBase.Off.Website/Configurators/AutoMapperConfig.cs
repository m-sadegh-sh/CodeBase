namespace CodeBase.Off.Website.Configurators {
    using System;
    using System.ServiceModel.Syndication;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Website.AutoMapping.Converters;
    using CodeBase.Off.Website.AutoMapping.Resolvers;
    using CodeBase.Off.Website.Models;

    public static class AutoMapperConfig {
        public static void BootUp() {
            CreateConverters();
            CreateEntityToModelMappers();
        }

        private static void CreateConverters() {
            Mapper.CreateMap<string, string>().ConvertUsing<NullOrEmptyStringToNotSpecifiedStringConverter>();
            Mapper.CreateMap<bool, string>().ConvertUsing<BoolToStringConverter>();
            Mapper.CreateMap<DateTime, string>().ConvertUsing<DateTimeToStringConverter>();
        }

        private static void CreateEntityToModelMappers() {
            Mapper.CreateMap<Entry, EntrySummaryModel>()
                .ForMember(esm => esm.CreateDate, mce => mce.MapFrom(e => e.CreateDateUtc))
                .ForMember(esm => esm.PrettyCreateDate, mce => mce.MapFrom(e => e.CreateDateUtc));

            Mapper.CreateMap<Entry, SearchResultModel>()
                .ForMember(srm => srm.CreateDate, mce => mce.MapFrom(e => e.CreateDateUtc))
                .ForMember(srm => srm.PrettyCreateDate, mce => mce.MapFrom(e => e.CreateDateUtc));

            Mapper.CreateMap<Entry, EntryShowModel>()
                .ForMember(esm => esm.Date, mce => mce.MapFrom(e => e.DateCreatedUtc))
                .ForMember(esm => esm.Html, mce => mce.ResolveUsing<MarkdownToHtmlResolver>().FromMember(e => e.Markdown));

            Mapper.CreateMap<EntryEditModel, Entry>()
                .ForMember(e => e.AuthorId, mce => mce.UseValue(IoC.Get<User>().Id))
                .ForMember(e => e.Tags, mce => mce.ResolveUsing<TagsFromTitleToIdResolver>());

            Mapper.CreateMap<Entry, EntryEditModel>()
                .ForMember(e => e.Tags, mce => mce.ResolveUsing<TagsFromTitleToIdResolver>());

            Mapper.CreateMap<Entry, SyndicationItem>().ConvertUsing<EntryToSyndicationItemConverter>();
        }
    }
}