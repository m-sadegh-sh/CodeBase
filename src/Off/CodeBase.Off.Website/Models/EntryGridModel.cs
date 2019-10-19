namespace CodeBase.Off.Website.Models {
    using System.Collections.Generic;
    using System.ComponentModel;

    public sealed class EntryGridModel {
        public string Slug { get; set; }

        [DisplayName(@"عنوان")]
        public string Title { get; set; }

        [DisplayName(@"نویسنده")]
        public UserSummaryModel Author { get; set; }

        [DisplayName(@"تعداد برچسب‌ها")]
        public int TagsCount { get; set; }

        [DisplayName(@"پیش نویس؟")]
        public string IsDraft { get; set; }

        [DisplayName(@"منتشر شده؟")]
        public string IsPublished { get; set; }

        [DisplayName(@"ترتیب نمایش")]
        public int Order { get; set; }

        [DisplayName(@"تعداد مشاهده")]
        public int ViewCount { get; set; }

        [DisplayName(@"ارسال شده؟")]
        public string WasSent { get; set; }
    }
}