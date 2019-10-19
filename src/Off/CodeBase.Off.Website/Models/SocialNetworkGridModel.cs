namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class SocialNetworkGridModel {
       public string Slug { get; set; }

        [DisplayName(@"نام")]
       public string Name { get; set; }

        [DisplayName(@"آدرس")]
         public string Url { get; set; }

        [DisplayName(@"فعال؟")]
        public string IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }

        [DisplayName(@"تعداد کلیک")]
        public int ClickCount { get; set; }
    }
}