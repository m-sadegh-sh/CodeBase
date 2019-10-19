namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class FriendLinkLogActionProvider : LogActionProviderBase<FriendLink> {
        protected override void BuildCore(FriendLink old, FriendLink @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(fl => fl.Slug)
                .AddIfChanged(fl => fl.Title)
                .AddIfChanged(fl => fl.Alt)
                .AddIfChanged(fl => fl.Url)
                .AddIfChanged(fl => fl.IsActive)
                .AddIfChanged(fl => fl.Order)
                .AddIfChanged(fl => fl.ClickCount);
        }
    }
}