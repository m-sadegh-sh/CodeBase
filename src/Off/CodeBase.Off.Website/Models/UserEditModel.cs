namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class UserEditModel {
        public int Id { get; set; }

        [DisplayName(@"نام کاربری")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
        public string UserName { get; set; }

        [DisplayName(@"کلمه عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(32, MinimumLength = 6, ErrorMessageResourceName = "Messages_InvalidMinMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Password { get; set; }

        [DisplayName(@"پست الکترونیکی")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(128, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceName = "Messages_InvalidEmail", ErrorMessageResourceType = typeof (Resources))]
        public string Email { get; set; }

        [DisplayName(@"نام")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string FriendlyName { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }
    }
}