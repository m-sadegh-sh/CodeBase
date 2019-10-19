namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;

    public sealed class LogActionShowModel {
        public string AffectedType { get; set; }
        public IDictionary<string, ValueSnapshotShowModel> Snapshots { get; set; }
    }
}