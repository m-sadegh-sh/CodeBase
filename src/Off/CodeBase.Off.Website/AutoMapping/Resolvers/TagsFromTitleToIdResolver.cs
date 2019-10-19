namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using System.Collections.Generic;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;

    public sealed class TagsFromTitleToIdResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var tags = source.Value.ToStringOrNull();

            if (tags == null)
                return source.New(string.Empty);

            var titlesList = tags.Split(';');
            var idsList = new List<int>();

            var tagService = IoC.Get<ITagService>();

            foreach (var title in titlesList) {
                if (tagService.Exists(title)) {
                    var dbTag = tagService.Get(title);

                    idsList.Add(dbTag.Id);
                } else {
                    var newId = tagService.Save(new Tag {
                        Title = tags
                    });

                    idsList.Add(newId);
                }
            }

            return source.New(idsList);
        }
    }
}