namespace CodeBase.Off.Core.Domain {
    using System;

    public sealed class HitInfo {
        public int Id { get; set; }
        public int HitId { get; set; }
        public string RequestIp { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime HitDateUtc { get; set; }

        public override string ToString() {
            return Id.ToString();
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj) {
            return Id == ((HitInfo) obj).Id;
        }
    }
}