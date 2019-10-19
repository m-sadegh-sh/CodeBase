namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;

    public sealed class EntryShowModel {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string CreateDate { get; set; }
        public string PrettyCreateDate { get; set; }
        public UserSummaryModel Author { get; set; }
        public string FullStory { get; set; }
        public IList<TagShowModel> Tags { get; set; }
        public int ViewCount { get; set; }
    }
}