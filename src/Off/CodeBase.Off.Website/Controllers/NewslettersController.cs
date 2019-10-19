namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Web.Mvc;

    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Services.Impl;
    using CodeBase.Off.Website.AutoMapping;
    using CodeBase.Off.Website.Mailers;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    public partial class NewslettersController : ControllerBase {
        private readonly IQueuedMailService _queuedMailService;
        private readonly NewslettersMailer _newslettersMailer;
        private readonly ISubscriptionService _subscriptionService;

        public NewslettersController(IQueuedMailService queuedMailService,
                                     NewslettersMailer newslettersMailer,
                                     ISubscriptionService subscriptionService) {
            _queuedMailService = queuedMailService;
            _newslettersMailer = newslettersMailer;
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public virtual ActionResult Index() {
            return View(Views.Index);
        }

        [HttpGet]
        [Then]
        public virtual ActionResult Subscrib() {
            var model = new SubscribRequestModel();

            if (IsPartialNeeded) {
                return PartialView(Views._Subscrib,
                                   model);
            }

            return View(Views.Subscrib,
                        model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Then]
        public virtual ActionResult Subscrib(SubscribRequestModel model) {
            if (!ModelState.IsValid) {
                return View(Views.Subscrib,
                            model);
            }

            var subscription = _subscriptionService.Get(model.Email);

            if (subscription != null) {
                if (subscription.IsConfirmed) {
                    ModelState.AddModelError("Email",
                                             Resources.Messages_Newsletters_Subscrib_DuplicateEmail);
                    return View(Views.Subscrib,
                                model);
                }
            } else {
                subscription = model.To<SubscribRequestModel, Subscription>();

                _subscriptionService.Save(subscription);
            }

            var message = _newslettersMailer.PrepareSubscribConfirmation(subscription);

            _queuedMailService.Save(message,
                                    CurrentConfig.Mails.NoReply.Id);

            subscription.IsPending = true;
            _subscriptionService.Save(subscription);

            SuccessNotification(string.Format(Resources.Messages_Newsletters_Subscrib_ConfirmationEmailSent,
                                              subscription.Email));
            return RedirectToThen();
        }

        [HttpPost]
        public virtual ActionResult SubscribValidateEmail(string email) {
            var subscription = _subscriptionService.Get(email);

            var isValid = subscription == null || !subscription.IsConfirmed;

            return Json(isValid);
        }

        [HttpGet]
        [Then]
        public virtual ActionResult SubscribConfirm(Guid guid) {
            if (!_subscriptionService.Exists(guid)) {
                ErrorNotification(Resources.Messages_Newsletters_GuidNotFound);
                return RedirectToThen();
            }

            var subscription = _subscriptionService.Get(guid);

            if (subscription.IsConfirmed) {
                WarningNotification(string.Format(Resources.Messages_Newsletters_SubscribConfirm_AlreadyConfirmed,
                                                  subscription.Email));
                return RedirectToThen();
            }

            subscription.IsPending = false;
            subscription.ConfirmDateUtc = DateTime.UtcNow;
            subscription.IsConfirmed = true;
            _subscriptionService.Save(subscription);

            SuccessNotification(string.Format(Resources.Messages_Newsletters_Subscrib_Confirmed,
                                              subscription.Email));
            return RedirectToThen();
        }

        [HttpGet]
        [Then]
        public virtual ActionResult UnSubscrib() {
            var model = new UnSubscribRequestModel();

            return View(Views.UnSubscrib,
                        model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Then]
        public virtual ActionResult UnSubscrib(UnSubscribRequestModel model) {
            if (!ModelState.IsValid) {
                return View(Views.UnSubscrib,
                            model);
            }

            var subscription = _subscriptionService.Get(model.Email);

            if (subscription == null) {
                ModelState.AddModelError("Email",
                                         Resources.Messages_Newsletters_UnSubscrib_NonExistentEmail);
                return View(Views.UnSubscrib,
                            model);
            }

            if (subscription.IsCancelled) {
                ModelState.AddModelError("Email",
                                         Resources.Messages_Newsletters_UnSubscribConfirm_AlreadyConfirmed);
                return View(Views.UnSubscrib,
                            model);
            }

            var message = _newslettersMailer.PrepareUnSubscribConfirmation(subscription);

            _queuedMailService.Save(message,
                                    CurrentConfig.Mails.NoReply.Id);

            subscription.IsPending = true;
            _subscriptionService.Save(subscription);

            SuccessNotification(string.Format(Resources.Messages_Newsletters_UnSubscrib_ConfirmationEmailSent,
                                              subscription.Email));
            return RedirectToThen();
        }

        [HttpPost]
        public virtual ActionResult UnSubscribValidateEmail(string email) {
            var subscription = _subscriptionService.Get(email);

            var isValid = subscription != null && !subscription.IsCancelled;

            return Json(isValid);
        }

        [HttpGet]
        [Then]
        public virtual ActionResult UnSubscribConfirm(Guid guid) {
            if (!_subscriptionService.Exists(guid)) {
                ErrorNotification(Resources.Messages_Newsletters_GuidNotFound);
                return RedirectToThen();
            }

            var subscription = _subscriptionService.Get(guid);

            subscription.IsPending = false;
            subscription.Email += " [CANCELLED]";
            subscription.CancelDateUtc = DateTime.UtcNow;
            subscription.IsCancelled = true;
            _subscriptionService.Save(subscription);

            SuccessNotification(Resources.Messages_Newsletters_UnSubscrib_Confirmed);
            return RedirectToThen();
        }
    }
}