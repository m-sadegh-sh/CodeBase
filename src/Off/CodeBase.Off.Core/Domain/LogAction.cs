namespace CodeBase.Off.Core.Domain {
    using System;
    using System.Collections.Generic;

    public sealed class LogAction {
        public Type AffectedType { get; set; }
        public IDictionary<string, ValueSnapshot> Snapshots { get; set; }
    }
}