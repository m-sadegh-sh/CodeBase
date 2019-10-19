namespace CodeBase.Off.Website.AutoMapping.Resolvers {
    using AutoMapper;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Website.Models;

    public sealed class UserIdToTeamMemberSummaryModelResolver : IValueResolver {
        public ResolutionResult Resolve(ResolutionResult source) {
            var userId = int.Parse(source.Value.ToString());

            var teamMemberService = IoC.Get<ITeamMemberService>();

            if (!teamMemberService.Exists(userId))
                return source.New(null);

            var teamMember = teamMemberService.Get(userId);

            var teamMemberSummaryModel = teamMember.To<TeamMember, TeamMemberSummaryModel>();

            return source.New(teamMemberSummaryModel);
        }
    }
}