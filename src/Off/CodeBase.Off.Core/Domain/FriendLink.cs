namespace CodeBase.Off.Core.Domain {
    public sealed class FriendLink {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int ClickCount { get; set; }
    }
}