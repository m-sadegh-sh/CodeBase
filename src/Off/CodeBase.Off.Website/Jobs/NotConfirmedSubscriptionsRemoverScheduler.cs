namespace CodeBase.Off.Website.Jobs {
    using CodeBase.Common.Infrastructure.Jobs;

    using Quartz;

    public sealed class NotConfirmedSubscriptionsRemoverScheduler : IJobScheduler {
        public void Schedule(IScheduler scheduler) {
            var details = JobBuilder.Create<NotConfirmedSubscriptionsRemoverJob>().
                                     WithIdentity(NotConfirmedSubscriptionsRemoverJob.Identity).
                                     Build();

            var trigger = TriggerBuilder.Create().
                                         WithIdentity(NotConfirmedSubscriptionsRemoverJob.Identity).
                                         WithSimpleSchedule(ssb => ssb.WithIntervalInHours(1).
                                                                       RepeatForever()).
                                         Build();

            scheduler.ScheduleJob(details,
                                  trigger);
        }
    }
}