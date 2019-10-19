namespace CodeBase.Off.Core.Domain {
    public sealed class Attribute {
        public object Owner { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}