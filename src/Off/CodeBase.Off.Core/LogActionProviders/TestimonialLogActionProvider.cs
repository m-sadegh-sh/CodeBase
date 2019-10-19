namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class TestimonialLogActionProvider : LogActionProviderBase<Testimonial> {
        protected override void BuildCore(Testimonial old, Testimonial @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(t => t.Slug)
                .AddIfChanged(t => t.FriendlyName)
                .AddIfChanged(t => t.Text)
                .AddIfChanged(t => t.Url)
                .AddIfChanged(t => t.IsActive)
                .AddIfChanged(t => t.Order)
                .AddIfChanged(t => t.ClickCount);
        }
    }
}