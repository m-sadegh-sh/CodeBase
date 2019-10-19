namespace CodeBase.Off.Core {
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Services.Impl;
    using CodeBase.Off.Core.Templating;
    using CodeBase.Off.Core.Utilities;

    using Ninject.Modules;
    using Ninject.Web.Common;

    public sealed class OffCoreModule : NinjectModule {
        public override void Load() {
            Bind<IConfigService>().To<ConfigService>().InRequestScope();
            Bind<Config>().ToMethod(c => c.GetService<IConfigService>().Current).InRequestScope();

            Bind<ILogService>().To<LogService>().InRequestScope();

            Bind<IUserService>().To<UserService>().InRequestScope();
            Bind<User>().ToMethod(c => c.GetService<IUserService>().Current).InRequestScope();
            Bind<ITeamMemberService>().To<TeamMemberService>().InRequestScope();

            Bind<ICarouselService>().To<CarouselService>().InRequestScope();

            Bind<ITagService>().To<TagService>().InRequestScope();
            Bind<IEntryService>().To<EntryService>().InRequestScope();
            Bind<ISubscriptionService>().To<SubscriptionService>().InRequestScope();

            Bind<IServiceService>().To<ServiceService>().InRequestScope();
            Bind<IPortfolioService>().To<PortfolioService>().InRequestScope();

            Bind<IFriendLinkService>().To<FriendLinkService>().InRequestScope();
            Bind<ISocialNetworkService>().To<SocialNetworkService>().InRequestScope();
            Bind<ITestimonialService>().To<TestimonialService>().InRequestScope();

            Bind<IAttributeService>().To<AttributeService>().InRequestScope();

            Bind<ITemplateBuilder<Entry>>().To<EntryTemplateBuilder>().InSingletonScope();

            Bind<ICloudService>().To<CloudService>().InRequestScope();
            Bind<IDateTimeHelper>().To<DateTimeHelper>().InRequestScope();
            Bind<IMessageService>().To<MessageService>().InRequestScope();
        }
    }
}