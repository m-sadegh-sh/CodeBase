namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class UserGridModel {
        public int Id { get; set; }

        [DisplayName(@"نام کاربری")]
        public string UserName { get; set; }

        [DisplayName(@"پست الکترونیکی")]
        public string Email { get; set; }

        [DisplayName(@"نام")]
        public string FriendlyName { get; set; }

        [DisplayName(@"تاریخ عضویت")]
        public string JoinDate { get; set; }
        public string PrettyJoinDate { get; set; }

        [DisplayName(@"تاریخ آخرین فعالیت")]
        public string LastActivityDate { get; set; }
        public string PrettyLastActivityDate { get; set; }

        [DisplayName(@"فعال؟")]
        public string IsActive { get; set; }

        [DisplayName(@"تعداد مطالب")]
        public int EntriesCount { get; set; }
    }
}