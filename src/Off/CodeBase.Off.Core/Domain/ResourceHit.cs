namespace CodeBase.Off.Core.Domain {
    public sealed class ResourceHit {
        public int Id { get; set; }
        public string Url { get; set; }

        public override string ToString() {
            return Id.ToString();
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj) {
            return Id == ((ResourceHit) obj).Id;
        }
    }
}