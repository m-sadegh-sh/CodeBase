namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class CarouselLogActionProvider : LogActionProviderBase<Carousel> {
        protected override void BuildCore(Carousel old, Carousel @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(c => c.Id)
                .AddIfChanged(c => c.Description)
                .AddIfChanged(c => c.IsActive)
                .AddIfChanged(c => c.Order);
        }
    }
}