namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class FriendLinkGridModel {
        public string Slug { get; set; }

        [DisplayName(@"عنوان")]
        public string Title { get; set; }

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