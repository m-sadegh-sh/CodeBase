namespace CodeBase.Off.Core.Domain {
    using System;
    using System.Net.Mail;

    public sealed class QueuedMail {
        public int Id { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailPriority SendPriority { get; set; }
        public int SendingTries { get; set; }
        public DateTime? SentDateUtc { get; set; }
        public int AccountConfigId { get; set; }

        public override string ToString() {
            return Id.ToString();
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj) {
            return Id == ((QueuedMail) obj).Id;
        }
    }
}