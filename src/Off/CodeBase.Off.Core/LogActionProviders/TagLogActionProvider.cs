namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class TagLogActionProvider : LogActionProviderBase<Tag> {
        protected override void BuildCore(Tag old, Tag @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(t => t.Id)
                .AddIfChanged(t => t.Title)
                .AddIfChanged(t => t.IsActive)
                .AddIfChanged(t => t.Order);
        }
    }
}