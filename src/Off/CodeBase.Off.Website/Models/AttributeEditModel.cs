namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    public sealed class AttributeEditModel {
        [DisplayName(@"شناسه مالک")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public object Owner { get; set; }

        [DisplayName(@"کلید")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
        public string Key { get; set; }

        [DisplayName(@"مقدار")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public object Value { get; set; }
    }
}