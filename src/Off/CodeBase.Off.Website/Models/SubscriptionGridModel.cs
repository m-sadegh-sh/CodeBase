namespace CodeBase.Off.Website.Models {
    using System;
    using System.ComponentModel;

    using FarsiLibrary.Utils;

    public sealed class SubscriptionGridModel {
        public Guid Guid { get; set; }

        [DisplayName(@"پست الکترونیکی")]
        public string Email { get; set; }

        [DisplayName(@"تاریخ عضویت")]
        public PersianDate SubscribDate { get; set; }

        public string PrettySubscribDate { get; set; }

        [DisplayName(@"ایمیل فعال‌سازی ارسال شده؟")]
        public string IsConfirmationSent { get; set; }

        [DisplayName(@"تائید شده؟")]
        public string IsConfirmed { get; set; }
    }
}