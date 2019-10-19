namespace CodeBase.Off.Core.Domain {
    public sealed class SocialNetwork {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int ClickCount { get; set; }
    }
}