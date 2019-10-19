namespace CodeBase.Off.Core.Domain {
    public sealed class ValueSnapshot {
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}