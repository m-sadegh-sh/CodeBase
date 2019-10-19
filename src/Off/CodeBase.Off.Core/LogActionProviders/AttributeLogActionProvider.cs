namespace CodeBase.Off.Core.LogActionProviders {
    using CodeBase.Off.Core.Domain;

    public sealed class AttributeLogActionProvider : LogActionProviderBase<Attribute> {
        protected override void BuildCore(Attribute old, Attribute @new, LogAction action) {
            WithInstance(action.Snapshots, old, @new)
                .AddIfChanged(a => a.Owner)
                .AddIfChanged(a => a.Key)
                .AddIfChanged(a => a.Value);
        }
    }
}