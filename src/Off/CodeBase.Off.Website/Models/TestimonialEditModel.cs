namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class TestimonialEditModel {
        [DisplayName(@"اسلاگ")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
        public string Slug { get; set; }

        [DisplayName(@"نام")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string FriendlyName { get; set; }

        [DisplayName(@"متن")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public string Text { get; set; }

        [DisplayName(@"آدرس")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [DataType(DataType.Url)]
        [DataAnnotationsExtensions.Url(ErrorMessageResourceName = "Messages_InvalidUrl", ErrorMessageResourceType = typeof (Resources))]
        public string Url { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int Order { get; set; }
    }
}