namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class CarouselGridModel {
        public int Id { get; set; }

        [DisplayName(@"عنوان")]
        public string Caption { get; set; }

        [DisplayName(@"فعال؟")]
        public string IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }
    }
}