namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class TeamMemberLogActionProvider : LogActionProviderBase<TeamMember> {
        protected override void BuildCore(TeamMember old, TeamMember @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(tm => tm.UserId)
                .AddIfChanged(tm => tm.FirstName)
                .AddIfChanged(tm => tm.LastName)
                .AddIfChanged(tm => tm.PublicEmail)
                .AddIfChanged(tm => tm.Position)
                .AddIfChanged(tm => tm.Biography)
                .AddIfChanged(tm => tm.IsActive)
                .AddIfChanged(tm => tm.Order)
                .AddIfChanged(tm => tm.ViewCount);
        }
    }
}