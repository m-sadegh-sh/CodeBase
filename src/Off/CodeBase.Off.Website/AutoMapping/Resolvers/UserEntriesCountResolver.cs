namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using System.Linq;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;

    public sealed class UserEntriesCountResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var userId = int.Parse(source.Value.ToString());

            var entryService = IoC.Get<IEntryService>();

            var entriesCount = entryService.GetList().
                                            Count(e => e.AuthorId == userId);

            return source.New(entriesCount);
        }
    }
}