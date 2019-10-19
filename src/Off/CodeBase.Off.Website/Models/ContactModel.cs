namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class ContactModel {
        [DisplayName(@"نام")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(32, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string SenderName { get; set; }

        [DisplayName(@"پست الکترونیکی")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [Email(ErrorMessageResourceName = "Messages_InvalidEmail", ErrorMessageResourceType = typeof (Resources))]
        public string SenderEmail { get; set; }

        [DisplayName(@"پیام")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public string Message { get; set; }
    }
}