namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class SocialNetworkLogActionProvider : LogActionProviderBase<SocialNetwork> {
        protected override void BuildCore(SocialNetwork old, SocialNetwork @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(sn => sn.Slug)
                .AddIfChanged(sn => sn.Name)
                .AddIfChanged(sn => sn.Url)
                .AddIfChanged(sn => sn.IsActive)
                .AddIfChanged(sn => sn.Order)
                .AddIfChanged(sn => sn.ClickCount);
        }
    }
}