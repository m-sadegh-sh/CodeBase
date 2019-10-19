namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;

    public sealed class TagsFromIdToTitleResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var idsList = (List<int>) source.Value;

            if (idsList == null || !idsList.Any())
                return source.New(string.Empty);

            var titlesList = new List<string>();

            var tagService = IoC.Get<ITagService>();

            foreach (var id in idsList) {
                if (tagService.Exists(id)) {
                    var dbTag = tagService.Get(id);

                    titlesList.Add(dbTag.Title);
                }
            }

            var titles = string.Join(";", titlesList);

            return source.New(titles);
        }
    }
}