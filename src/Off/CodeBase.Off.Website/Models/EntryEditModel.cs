namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class EntryEditModel {
        [DisplayName(@"اسلاگ")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(512, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessageResourceName = "Messages_InvalidSlug", ErrorMessageResourceType = typeof (Resources))]
        public string Slug { get; set; }

        [DisplayName(@"عنوان")]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        [StringLength(1024, ErrorMessageResourceName = "Messages_InvalidMaxLength", ErrorMessageResourceType = typeof (Resources))]
        public string Title { get; set; }

        [DisplayName(@"خلاصه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public string Abstract { get; set; }

        [DisplayName(@"متن کامل")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Required(ErrorMessageResourceName = "Messages_Required", ErrorMessageResourceType = typeof (Resources))]
        public string FullStory { get; set; }

        [DisplayName(@"برچسب‌ها")]
        public IList<TagShowModel> Tags { get; set; }

        [DisplayName(@"پیش نویس؟")]
        public bool IsDraft { get; set; }

        [DisplayName(@"منتشر شده؟")]
        public bool IsPublished { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        [Integer(ErrorMessageResourceName = "Messages_InvalidInteger", ErrorMessageResourceType = typeof (Resources))]
        public int Order { get; set; }
    }
}