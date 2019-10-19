namespace CodeBase.Off.Website.Controllers {
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Web.Mvc;
    using CodeBase.Off.Website.Attributes;
    using CodeBase.Off.Website.Jobs;
    using CodeBase.Off.Website.Properties;

    using Quartz;

    public sealed class AdminController : ControllerBase {
        [HttpGet]
        [Admin]
        [Then]
        public ActionResult Backup() {
            var scheduler = IoC.Get<IScheduler>();

            scheduler.TriggerJob(JobKey.Create(CloudBackupJob.Identity));

            var details = scheduler.GetJobDetail(JobKey.Create(CloudBackupJob.Identity));

            var result = details.Description;

            if (result != null)
                SuccessNotification(string.Format(Resources.Messages_BackupComplete, result));
            else
                ErrorNotification(Resources.Messages_BackupFailure);

            return RedirectToThen();
        }
    }
}