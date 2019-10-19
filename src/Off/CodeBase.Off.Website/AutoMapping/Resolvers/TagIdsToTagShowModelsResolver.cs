namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.Models;

    public sealed class TagIdsToTagShowModelsResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var ids = (IList<int>) source.Value;

            var tagShowModels = new List<TagShowModel>();

            if (!ids.Any())
                return source.New(tagShowModels);

            var tagService = IoC.Get<ITagService>();

            foreach (var id in ids) {
                if (tagService.Exists(id)) {
                    var tag = tagService.Get(id);

                    var tagShowModel = tag.To<Tag, TagShowModel>();

                    tagShowModels.Add(tagShowModel);
                }
            }

            return source.New(tagShowModels);
        }
    }
}