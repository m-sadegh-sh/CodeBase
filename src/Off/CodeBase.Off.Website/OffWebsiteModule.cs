namespace CodeBase.Off.Website {
    using CodeBase.Common.Infrastructure.DependencyResolution;

    using MarkdownSharp;

    using Ninject.Modules;

    using Quartz;
    using Quartz.Impl;

    public sealed class OffWebsiteModule : NinjectModule {
        public override void Load() {
            Bind<ISchedulerFactory>().ToMethod(c => new StdSchedulerFactory()).InSingletonScope();
            Bind<IScheduler>().ToMethod(c => c.GetService<ISchedulerFactory>().GetScheduler()).InSingletonScope();
            Bind<Markdown>().To<Markdown>().InSingletonScope();
        }
    }
}