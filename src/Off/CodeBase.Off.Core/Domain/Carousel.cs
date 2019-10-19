namespace CodeBase.Off.Core.Domain {
    public sealed class Carousel {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}