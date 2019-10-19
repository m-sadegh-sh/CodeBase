namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class ServiceLogActionProvider : LogActionProviderBase<Service> {
        protected override void BuildCore(Service old, Service @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(s => s.Slug)
                .AddIfChanged(s => s.Title)
                .AddIfChanged(s => s.Description)
                .AddIfChanged(s => s.IsActive)
                .AddIfChanged(s => s.Order);
        }
    }
}