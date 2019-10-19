namespace CodeBase.Off.Website.Configurators {
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Infrastructure.Storage.Json;
    using CodeBase.Off.Core.Domain;

    public static class KeyRegisterar {
        public static void RegisterKeys() {
            var keyProvider = IoC.Get<JsonKeyProvider>();

            keyProvider.RegisterKey<Config>(c => c.BlogId);
            keyProvider.RegisterKey<Log>(l => l.Id);

            keyProvider.RegisterKey<User>(u => u.Id);
            keyProvider.RegisterKey<TeamMember>(tm => tm.UserId);

            keyProvider.RegisterKey<Carousel>(c => c.Id);

            keyProvider.RegisterKey<Tag>(t => t.Id);
            keyProvider.RegisterKey<Entry>(e => e.Slug);

            keyProvider.RegisterKey<Service>(s => s.Slug);
            keyProvider.RegisterKey<Portfolio>(p => p.Slug);

            keyProvider.RegisterKey<FriendLink>(fl => fl.Slug);
            keyProvider.RegisterKey<SocialNetwork>(sn => sn.Slug);
            keyProvider.RegisterKey<Testimonial>(t => t.Slug);

            keyProvider.RegisterKey<Attribute<string>>(a => a.Owner, a => a.Key);

            keyProvider.RegisterKey<Subscription>(s => s.Guid);
        }
    }
}