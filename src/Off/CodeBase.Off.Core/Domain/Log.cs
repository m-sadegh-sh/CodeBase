namespace CodeBase.Off.Core.Domain {
    using System;
    using System.Collections.Generic;

    public sealed class Log {
        public int Id { get; internal set; }
        public SeverityLevel Level { get; set; }
        public DateTime LogDateUtc { get; set; }
        public string RawUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string IpInfo { get; set; }
        public IDictionary<string, string> Params { get; set; }
        public LogAction Action { get; set; }
        public string UserInfo { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public enum SeverityLevel {
            Debug,
            Info,
            Warning,
            Error,
            Crud
        }
    }
}