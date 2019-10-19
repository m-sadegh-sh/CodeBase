namespace CodeBase.Off.Core.Domain {
    public sealed class Tag {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}