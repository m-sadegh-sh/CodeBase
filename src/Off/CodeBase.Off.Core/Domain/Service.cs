namespace CodeBase.Off.Core.Domain {
    public sealed class Service {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}