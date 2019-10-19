namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class TeamMemberGridModel {
        public int UserId { get; set; }

        [DisplayName(@"نام")]
        public string FirstName { get; set; }

        [DisplayName(@"نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName(@"پست الکترونیکی (عمومی)")]
        public string PublicEmail { get; set; }

        [DisplayName(@"مسند")]
        public string Position { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }

        [DisplayName(@"تعداد مشاهده")]
        public int ViewCount { get; set; }
    }
}