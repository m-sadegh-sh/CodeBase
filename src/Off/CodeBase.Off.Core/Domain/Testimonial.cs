namespace CodeBase.Off.Core.Domain {
    public sealed class Testimonial {
        public string Slug { get; set; }
        public string FriendlyName { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int ClickCount { get; set; }
    }
}