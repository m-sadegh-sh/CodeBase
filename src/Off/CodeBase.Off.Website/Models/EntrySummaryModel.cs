namespace CodeBase.Off.Website.Models {
    public sealed class EntrySummaryModel {
        public string Slug { get; set; }
        public string Title { get; set; }
        public UserSummaryModel Author { get; set; }
        public string CreateDate { get; set; }
        public string PrettyCreateDate { get; set; }
        public string Abstract { get; set; }
    }
}