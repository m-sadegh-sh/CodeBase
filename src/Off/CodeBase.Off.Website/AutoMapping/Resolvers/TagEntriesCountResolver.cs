namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using System.Linq;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;

    public sealed class TagEntriesCountResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var tagId = int.Parse(source.Value.ToString());

            var entryService = IoC.Get<IEntryService>();

            var entriesCount = entryService.GetList().
                                            Sum(e => e.Tags.Count(t => t == tagId));

            return source.New(entriesCount);
        }
    }
}