namespace CodeBase.Off.Website.Controllers {
    using System.Net.Mail;
    using System.Web.Mvc;

    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Utilities;
    using CodeBase.Off.Website.Models;
    using CodeBase.Off.Website.Properties;

    public sealed class ContactController : ControllerBase {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService) {
            _messageService = messageService;
        }

        [HttpGet]
        public ActionResult Index() {
            var model = new ContactModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Then]
        public ActionResult Index(ContactModel model) {
            if (!ModelState.IsValid)
                return View();

            var from = new MailAddress(model.SenderEmail,
                                       model.SenderName);

            var to = new MailAddress(CurrentConfig.ContactForm.RecipientEmail,
                                     CurrentConfig.ContactForm.RecipientName);

            var mailMessage = new MailMessage(from, to) {
                Subject = CurrentConfig.ContactForm.Subject,
                Body = model.Message,
                IsBodyHtml = false
            };

            if (_messageService.SendEmail(mailMessage)) {
                SuccessNotification(Resources.Messages_SendMessageComplete);
                return RedirectToThen();
            }

            ErrorNotification(Resources.Messages_SendMessageFailure);
            return View(model);
        }
    }
}