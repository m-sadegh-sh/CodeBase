namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class SubscriptionLogActionProvider : LogActionProviderBase<Subscription> {
        protected override void BuildCore(Subscription old, Subscription @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(s => s.Guid)
                .AddIfChanged(s => s.Email)
                .AddIfChanged(s => s.IsConfirmed);
        }
    }
}