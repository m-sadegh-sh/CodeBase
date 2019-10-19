namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class ConfigEditModel {
        public string Key { get; set; }

        [DisplayName(@"عنوان")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Heading { get; set; }

        [DisplayName(@"عنوان ثانویه")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Tagline { get; set; }

        [DisplayName(@"قالب جاری")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Template { get; set; }

        [DisplayName(@"تعداد رکوردهای قابل نمایش در هر صفحه")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int PageSize { get; set; }

        [DisplayName(@"منطقه زمانی پیشفرض")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string DefaultTimeZoneId { get; set; }

        [DisplayName(@"امکان انتخاب منطقه زمانی توسط کاربر")]
        public bool AllowUsersToSetTimeZone { get; set; }

        [DisplayName(@"بستن سایت")]
        public bool IsClosed { get; set; }

        [DisplayName(@"دلیل بستن")]
        [StringLength(1024, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string CloseReason { get; set; }

        public IList<UserSummaryModel> Admins { get; set; }

        public MailsConfigEditModel Mails { get; set; }

        public CloudConfigEditModel Cloud { get; set; }

        public SeoConfigEditModel Seo { get; set; }

        public sealed class MailsConfigEditModel {
            [DisplayName(@"پست الکترونیکی (فرم تماس با ما)")]
            public MailAccountConfigEditModel Contact { get; set; }

            [DisplayName(@"پست الکترونیکی (خبرنامه)")]
            public MailAccountConfigEditModel Newsletter { get; set; }

            public sealed class MailAccountConfigEditModel {
                public int Id { get; set; }

                public string DisplayName { get; set; }

                public string Email { get; set; }

                public string UserName { get; set; }

                public string Password { get; set; }

                public string Host { get; set; }

                public short Port { get; set; }

                public bool SslEnabled { get; set; }
            }
        }

        public sealed class CloudConfigEditModel {
            [DisplayName(@"کلید عمومی مصرف‌کننده")]
            [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string ConsumerKey { get; set; }

            [DisplayName(@"کلید مخفی مصرف‌کننده")]
            [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string ConsumerSecret { get; set; }

            [DisplayName(@"نام کاربری")]
            [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string UserName { get; set; }

            [DisplayName(@"گذرواژه")]
            [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string Password { get; set; }

            [DisplayName(@"مسیر پوشه پشتیبان")]
            [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string BackupFolder { get; set; }
        }

        public sealed class SeoConfigEditModel {
            [DisplayName(@"آدرس سی‌دی‌ان")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [DataType(DataType.Url)]
            [DataAnnotationsExtensions.Url(ErrorMessageResourceName = "Messages_InvalidUrl", ErrorMessageResourceType = typeof (Resources))]
            public string CdnUrl { get; set; }

            [DisplayName(@"مسیر عکس‌ها")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string ImagesPath { get; set; }

            [DisplayName(@"مسیر لس‌ها")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string LessPath { get; set; }

            [DisplayName(@"مسیر استایل‌ها")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string StylesPath { get; set; }

            [DisplayName(@"مسیر اسکریپت‌ها")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string ScriptsPath { get; set; }

            [DisplayName(@"مسیر فونت‌ها")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
            public string FontsPath { get; set; }

            [DisplayName(@"عنوان صفحات (پیشفرض)")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string DefaultTitle { get; set; }

            [DisplayName(@"کلمات کلیدی صفحات (پیشفرض)")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(256, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string DefaultKeywords { get; set; }

            [DisplayName(@"توضیحات صفحات (پیشفرض)")]
            [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
            [StringLength(256, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string DefaultDescription { get; set; }

            [DisplayName(@"جداکننده (عنوان صفحات)")]
            [StringLength(256, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string TitleSeparator { get; set; }

            [DisplayName(@"جداکننده (مسیر جاری)")]
            [StringLength(256, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string BreadcrumbSeparator { get; set; }

            [DisplayName(@"شناسه گوگل آنالیتیکس")]
            [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
            public string GoogleAnalyticsId { get; set; }
        }
    }
}