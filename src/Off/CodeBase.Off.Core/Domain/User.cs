namespace CodeBase.Off.Core.Domain {
    using System;

    using CodeBase.Common.Infrastructure.Application;

    public sealed class User {
        public int Id { get; internal set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FriendlyName { get; set; }
        public DateTime JoinDateUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public bool IsActive { get; set; }

        public sealed class Attributes {
            public static readonly string TimeZoneId = ReflectionExtensions.GetMemberName<Attributes, string>(a => TimeZoneId);
        }
    }
}