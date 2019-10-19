namespace CodeBase.Off.Website.Jobs {
    using CodeBase.Common.Infrastructure.Jobs;

    using Quartz;

    public sealed class NotSentQueuedMailsSenderScheduler : IJobScheduler {
        public void Schedule(IScheduler scheduler) {
            var details = JobBuilder.Create<NotSentQueuedMailsSenderJob>().
                                     WithIdentity(NotSentQueuedMailsSenderJob.Identity).
                                     Build();

            var trigger = TriggerBuilder.Create().
                                         WithIdentity(NotSentQueuedMailsSenderJob.Identity).
                                         WithSimpleSchedule(ssb => ssb.WithIntervalInMinutes(5).
                                                                       RepeatForever()).
                                         Build();

            scheduler.ScheduleJob(details,
                                  trigger);
        }
    }
}