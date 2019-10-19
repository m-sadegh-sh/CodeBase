namespace CodeBase.Off.Core.Domain {
    using System;

    public sealed class Subscription {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
    }
}