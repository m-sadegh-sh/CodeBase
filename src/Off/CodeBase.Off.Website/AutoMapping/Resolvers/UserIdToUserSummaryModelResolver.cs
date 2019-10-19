namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.Models;

    public sealed class UserIdToUserSummaryModelResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var userId = int.Parse(source.Value.ToString());

            var userService = IoC.Get<IUserService>();

            if (!userService.Exists(userId))
                return source.New(null);

            var user = userService.Get(userId);

            var userSummaryModel = user.To<User, UserSummaryModel>();

            return source.New(userSummaryModel);
        }
    }
}