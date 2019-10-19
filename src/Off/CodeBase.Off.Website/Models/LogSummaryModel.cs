namespace CodeBase.Off.Website.Models {
    public sealed class LogSummaryModel {
        public int Id { get; set; }
        public string Level { get; set; }
        public string RawUrl { get; set; }
        public string IpInfo { get; set; }
        public string UserInfo { get; set; }
    }
}