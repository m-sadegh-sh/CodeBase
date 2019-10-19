namespace CodeBase.Common.Web {
    using System.Security.Principal;
    using System.Web;
    using System.Web.Caching;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Extensions;
    using CodeBase.Common.Web.Mvc.Layout;

    using Ninject.Modules;
    using Ninject.Web.Common;

    public sealed class CommonWebModule : NinjectModule {
        public override void Load() {
            Bind<HttpRequestBase>().ToMethod(c => c.GetService<HttpContext>().Request.AsBase()).InTransientScope();
            Bind<HttpRequest>().ToMethod(c => c.GetService<HttpContext>().Request).InTransientScope();
            Bind<HttpResponse>().ToMethod(c => c.GetService<HttpContext>().Response).InTransientScope();
            Bind<HttpServerUtility>().ToMethod(c => c.GetService<HttpContext>().Server).InTransientScope();
            Bind<Cache>().ToMethod(c => c.GetService<HttpContext>().Cache).InTransientScope();
            Bind<IPrincipal>().ToMethod(c => c.GetService<HttpContext>().User).InTransientScope();
            Bind<IIdentity>().ToMethod(c => c.GetService<IPrincipal>() != null ? c.GetService<IPrincipal>().Identity : new WindowsIdentity("")).InTransientScope();
            Bind<ILayoutHelpers>().To<LayoutHelpers>().InRequestScope();
        }
    }
}