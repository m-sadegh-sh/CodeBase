namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class TagEditModel {
        public int Id { get; set; }

        [DisplayName(@"عنوان")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(1024, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Title { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int Order { get; set; }
    }
}