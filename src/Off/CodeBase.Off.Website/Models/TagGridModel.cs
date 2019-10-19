namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class TagGridModel {
        public int Id { get; set; }

        [DisplayName(@"عنوان")]
        public string Title { get; set; }

        [DisplayName(@"فعال؟")]
        public string IsActive { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }

        [DisplayName(@"تعداد مطالب")]
        public int EntriesCount { get; set; }
    }
}