namespace CodeBase.Off.Core.Domain {
    using System;
    using System.Collections.Generic;

    public sealed class Entry {
        public string Slug { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; internal set; }
        public DateTime CreateDateUtc { get; internal set; }
        public string Abstract { get; set; }
        public string FullStory { get; set; }
        public IList<int> Tags { get; set; }
        public bool IsDraft { get; set; }
        public bool IsPublished { get; set; }
        public int Order { get; set; }
        public int ViewCount { get; set; }
        public bool WasSent { get; set; }
    }
}