namespace CodeBase.Off.Website.Jobs {
    using CodeBase.Common.Infrastructure.Jobs;

    using Quartz;

    public sealed class CloudBackupScheduler : IJobScheduler {
        public void Schedule(IScheduler scheduler) {
            var details = JobBuilder.Create<CloudBackupJob>()
                .WithIdentity(CloudBackupJob.Identity).Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(CloudBackupJob.Identity)
                .WithCronSchedule("0 0 17 ? * *")
                .Build();

            scheduler.ScheduleJob(details, trigger);
        }
    }
}