namespace CodeBase.Off.Website.Jobs {
    using CodeBase.Common.Infrastructure.Jobs;

    using Quartz;

    public sealed class EntriesSenderScheduler : IJobScheduler {
        public void Schedule(IScheduler scheduler) {
            var details = JobBuilder.Create<EntriesSenderJob>()
                .WithIdentity(EntriesSenderJob.Identity).Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(EntriesSenderJob.Identity)
                .WithCronSchedule("0 0 17 ? * *")
                .Build();

            scheduler.ScheduleJob(details, trigger);
        }
    }
}