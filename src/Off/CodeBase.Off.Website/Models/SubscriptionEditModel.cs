namespace CodeBase.Off.Website.Models {
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class SubscriptionEditModel {
        public Guid Guid { get; set; }

        [DisplayName(@"عنوان")]
        [StringLength(2048, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceName = "Messages_InvalidEmail", ErrorMessageResourceType = typeof (Resources))]
        public string Email { get; set; }

        [DisplayName(@"تائید شده؟")]
        public bool IsConfirmed { get; set; }
    }
}