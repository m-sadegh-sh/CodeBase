namespace CodeBase.Off.Core.Domain {
    using System.Collections.Generic;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;

    public sealed class Config {
        public string BlogId { get; set; }
        public string Heading { get; set; }
        public string Tagline { get; set; }
        public string Crossbar { get; set; }
        public string Template { get; set; }
        public int PageSize { get; set; }
        public string DefaultTimeZoneId { get; set; }
        public bool AllowUsersToSetTimeZone { get; set; }
        public List<int> Admins { get; set; }
        public MailsConfig Mails { get; set; }
        public ContactFormConfig ContactForm { get; set; }
        public CloudConfig Cloud { get; set; }
        public SeoConfig Seo { get; set; }

        public bool IsAdmin(int id) {
            return Admins.Contains(id);
        }

        public bool IsAdmin(string userName) {
            var userService = IoC.Get<IUserService>();

            var user = userService.Get(userName);

            return Admins.Contains(user.Id);
        }

        public sealed class MailsConfig {
            public string Contact { get; set; }
            public string Newsletter { get; set; }
        }

        public sealed class ContactFormConfig {
            public string RecipientName { get; set; }
            public string Subject { get; set; }
        }

        public sealed class CloudConfig {
            public string ConsumerKey { get; set; }
            public string ConsumerSecret { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string BackupFolder { get; set; }
        }

        public sealed class SeoConfig {
            public string CdnUrl { get; set; }
            public string ImagesPath { get; set; }
            public string LessPath { get; set; }
            public string StylesPath { get; set; }
            public string ScriptsPath { get; set; }
            public string FontsPath { get; set; }
            public string DefaultTitle { get; set; }
            public string DefaultKeywords { get; set; }
            public string DefaultDescription { get; set; }
            public string TitleSeparator { get; set; }
            public string BreadcrumbSeparator { get; set; }
            public string GoogleAnalyticsId { get; set; }
        }
    }
}