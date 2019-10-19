namespace CodeBase.Off.Website {
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using CodeBase.Off.Website.Configurators;

    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            KeyRegisterar.RegisterKeys();

            JobRunner.StartJobs();

            BundleRegisterar.RegisterBundles();
            RouteRegisterar.RegisterRoutes();
            AutoMapperConfig.BootUp();
            ClientValidationExtensionsRegistrar.RegisterExstensions();

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            EnforceLowercaseUrl();
        }

        private void EnforceLowercaseUrl() {
            var path = Request.Url.AbsolutePath;
            var verbIsGet = string.Equals(Request.HttpMethod, HttpVerbs.Get.ToString(), StringComparison.CurrentCultureIgnoreCase);

            if (!verbIsGet || !path.Any(char.IsUpper))
                return;

            Response.RedirectPermanent(path.ToLowerInvariant() + Request.Url.Query);
        }

        protected void Application_Error(object sender, EventArgs e) {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException ?? new HttpException(null, exception);

            //Server.ClearError();

            //Context.ExecuteAction("Error", "Index", new
            //{
            //    httpException
            //});
        }
    }
}