namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class PortfolioLogActionProvider : LogActionProviderBase<Portfolio> {
        protected override void BuildCore(Portfolio old, Portfolio @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(p => p.Slug)
                .AddIfChanged(p => p.Title)
                .AddIfChanged(p => p.Description)
                .AddIfChanged(p => p.IsActive)
                .AddIfChanged(p => p.Order);
        }
    }
}