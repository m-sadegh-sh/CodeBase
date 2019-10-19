namespace CodeBase.Common.Infrastructure {
    using System.Configuration;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Common.Infrastructure.Storage.Json;

    using Ninject.Modules;
    using Ninject.Web.Common;

    public sealed class CommonInfrastructureModule : NinjectModule {
        public override void Load() {
            Bind<JsonKeyProvider>().ToSelf().InSingletonScope();
            Bind<JsonPathProvider>().ToSelf().InSingletonScope().WithConstructorArgument("basePath", ConfigurationManager.AppSettings["dataStorageBasePath"]);

            Bind<IRepository>().To<JsonRepository>().InRequestScope();
        }
    }
}