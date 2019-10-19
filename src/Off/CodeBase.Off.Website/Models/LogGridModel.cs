namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class LogGridModel {
        public int Id { get; set; }

        [DisplayName(@"سطح")]
        public string Level { get; set; }

        [DisplayName(@"آدرس")]
        public string RawUrl { get; set; }

        [DisplayName(@"آی‌پی")]
        public string IpInfo { get; set; }

        [DisplayName(@"توسط")]
        public string UserInfo { get; set; }
    }
}