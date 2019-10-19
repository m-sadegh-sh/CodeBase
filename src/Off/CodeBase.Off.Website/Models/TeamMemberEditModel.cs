namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class TeamMemberEditModel {
        public int UserId { get; set; }

        [DisplayName(@"نام")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string FirstName { get; set; }

        [DisplayName(@"نام خانوادگی")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string LastName { get; set; }

        [DisplayName(@"پست الکترونیکی (عمومی)")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceName = "Messages_InvalidEmail", ErrorMessageResourceType = typeof (Resources))]
        public string PublicEmail { get; set; }

        [DisplayName(@"مسند")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(64, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Position { get; set; }

        [DisplayName(@"بیوگرافی")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public string Biography { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int Order { get; set; }
    }
}