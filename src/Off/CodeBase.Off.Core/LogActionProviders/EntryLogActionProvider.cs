namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class EntryLogActionProvider : LogActionProviderBase<Entry> {
        protected override void BuildCore(Entry old, Entry @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(e => e.Slug)
                .AddIfChanged(e => e.Title)
                .AddIfChanged(e => e.AuthorId)
                .AddIfChanged(e => e.CreateDateUtc)
                .AddIfChanged(e => e.Abstract)
                .AddIfChanged(e => e.FullStory)
                .AddIfChanged(e => e.Tags)
                .AddIfChanged(e => e.IsDraft)
                .AddIfChanged(e => e.IsPublished)
                .AddIfChanged(e => e.Order)
                .AddIfChanged(e => e.ViewCount)
                .AddIfChanged(e => e.WasSent);
        }
    }
}