namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class FriendLinkEditModel {
        [DisplayName(@"اسلاگ")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
        public string Slug { get; set; }

        [DisplayName(@"عنوان")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(1024, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Title { get; set; }

        [DisplayName(@"عنوان (ثانویه)")]
        [StringLength(1024, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Alt { get; set; }

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