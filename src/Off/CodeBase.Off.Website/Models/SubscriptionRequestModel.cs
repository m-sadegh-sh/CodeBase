namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class SubscriptionRequestModel {
        [DisplayName(@"پست الکترونیکی")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(2048, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceName = "Messages_InvalidEmail", ErrorMessageResourceType = typeof (Resources))]
        [Remote("SubscribValidateEmail", "Newsletters", HttpMethod = "POST", ErrorMessageResourceName = "Messages_Newsletters_Subscrib_DuplicateEmail", ErrorMessageResourceType = typeof (Resources))]
        public string Email { get; set; }
    }
}