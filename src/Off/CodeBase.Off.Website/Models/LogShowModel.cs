namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;

    public sealed class LogShowModel {
        public int Id { get; set; }
        public string Level { get; set; }
        public string LogDate { get; set; }
        public string PrettyLogDate { get; set; }
        public string RawUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string IpInfo { get; set; }
        public IDictionary<string, string> Params { get; set; }
        public LogActionSummaryModel Action { get; set; }
        public string UserInfo { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}