namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;
    using System.Web.Routing;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Mvc;
    using CodeBase.Common.Web.Routing;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;

    using IOFile = System.IO.File;

    public abstract class ControllerBase : Controller {
        protected override void Initialize(RequestContext requestContext) {
            base.Initialize(requestContext);

            LogService = IoC.Get<ILogService>();
            CurrentUser = IoC.Get<User>();
            CurrentConfig = IoC.Get<Config>();
        }

        protected ILogService LogService { get; set; }

        protected User CurrentUser { get; private set; }

        protected Config CurrentConfig { get; private set; }

        protected bool IsPartialNeeded {
            get { return ControllerContext.IsChildAction || Request.IsAjaxRequest(); }
        }

        protected EmptyResult Empty() {
            return new EmptyResult();
        }

        protected RssResult Rss(string title,
                                string description,
                                Uri uri,
                                IList<SyndicationItem> items,
                                string language = "fa-IR") {
            return new RssResult(title,
                                 description,
                                 uri,
                                 items,
                                 language);
        }

        protected RedirectResult RedirectToThen(bool referrerAllowed = false,
                                                string defaultRouteName = null,
                                                object defaultRouteValues = null) {
            if (ViewBag.Then == null)
                throw new InvalidOperationException("Parameter \"Then\" couldn't be found.");

            var then = Server.UrlDecode(ViewBag.Then);

            if (Url.IsLocalUrl(then))
                return Redirect(then);

            if (referrerAllowed && Request.UrlReferrer != null) {
                if (Url.IsLocalUrl(Request.UrlReferrer.ToString())) {
                    var referrerUrl = Request.UrlReferrer.AbsolutePath;

                    return Redirect(referrerUrl);
                }
            }

            if (defaultRouteName.AsNullIfEmpty() != null) {
                var defaultRouteUrl = defaultRouteName.GetUrl(defaultRouteValues);

                return Redirect(defaultRouteUrl);
            }

            var url = Request.Url.AbsolutePath;

            return Redirect(url);
        }

        protected FileStreamResult Pdf(string fileName,
                                       string fileDownloadName) {
            var resumeFile = IOFile.OpenRead(fileName);
            return File(resumeFile,
                        "application/pdf",
                        fileDownloadName);
        }

        protected virtual void InfoNotification(string message,
                                                bool persistForTheNextRequest = true) {
            AddNotification(NotifyType.Info,
                            message,
                            persistForTheNextRequest);
        }

        protected virtual void SuccessNotification(string message,
                                                   bool persistForTheNextRequest = true) {
            AddNotification(NotifyType.Success,
                            message,
                            persistForTheNextRequest);
        }

        protected virtual void WarningNotification(string message,
                                                   bool persistForTheNextRequest = true) {
            AddNotification(NotifyType.Warning,
                            message,
                            persistForTheNextRequest);
        }

        protected virtual void ErrorNotification(string message,
                                                 bool persistForTheNextRequest = true) {
            AddNotification(NotifyType.Error,
                            message,
                            persistForTheNextRequest);
        }

        protected virtual void ExceptionNotification(Exception exception,
                                                     bool persistForTheNextRequest = true) {
            AddNotification(NotifyType.Error,
                            exception.Message,
                            persistForTheNextRequest);
        }

        protected virtual void AddNotification(NotifyType notifyType,
                                               string message,
                                               bool persistForTheNextRequest) {
            var dataKey = string.Format("notifications.{0}",
                                        notifyType);

            IDictionary<string, object> dictionary;

            if (persistForTheNextRequest)
                dictionary = TempData;
            else
                dictionary = ViewData;

            if (dictionary[dataKey] == null)
                dictionary[dataKey] = new List<string>();
            ((List<string>) dictionary[dataKey]).Add(message);
        }
    }
}