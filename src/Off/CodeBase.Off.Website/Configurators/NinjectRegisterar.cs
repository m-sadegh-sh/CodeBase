using CodeBase.Off.Website.Configurators;

using WebActivator;

[assembly: PreApplicationStartMethod(typeof (NinjectRegisterar), "Register")]
[assembly: ApplicationShutdownMethod(typeof (NinjectRegisterar), "Release")]

namespace CodeBase.Off.Website.Configurators {
    using System;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure;
    using CodeBase.Common.Web;
    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Core;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using NinjectAdapter;

    public static class NinjectRegisterar {
        private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

        public static void Register() {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            _bootstrapper.Initialize(CreateKernel);
        }

        public static void Release() {
            _bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel) {
            kernel.Load<CommonInfrastructureModule>();
            kernel.Load<CommonWebModule>();
            kernel.Load<OffCoreModule>();
            kernel.Load<OffWebsiteModule>();

            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(_bootstrapper.Kernel));
            DependencyResolver.SetResolver(new NinjectDependencyResolver(_bootstrapper.Kernel));
        }
    }
}