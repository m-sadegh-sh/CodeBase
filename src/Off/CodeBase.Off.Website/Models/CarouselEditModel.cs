namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class CarouselEditModel {
        public int Id { get; set; }

        [DisplayName(@"عنوان")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [StringLength(2048, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Caption { get; set; }

        [DisplayName(@"فعال؟")]
        public bool IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int Order { get; set; }
    }
}