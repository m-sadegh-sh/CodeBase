namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class UserLogActionProvider : LogActionProviderBase<User> {
        protected override void BuildCore(User old, User @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(u => u.Id)
                .AddIfChanged(u => u.UserName)
                .AddIfChanged(u => u.Email)
                .AddIfChanged(u => u.FriendlyName)
                .AddIfChanged(u => u.JoinDateUtc)
                .AddIfChanged(u => u.LastActivityDateUtc)
                .AddIfChanged(u => u.IsActive);
        }
    }
}