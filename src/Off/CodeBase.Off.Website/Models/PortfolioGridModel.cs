namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Properties;

    using DataAnnotationsExtensions;

    public sealed class PortfolioGridModel {
         public string Slug { get; set; }

        [DisplayName(@"عنوان")]
       public string Title { get; set; }

        [DisplayName(@"فعال؟")]
        public string IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }
    }
}