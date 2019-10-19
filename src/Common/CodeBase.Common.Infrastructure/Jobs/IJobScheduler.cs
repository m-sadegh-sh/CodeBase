namespace CodeBase.Common.Infrastructure.Jobs {
    using Quartz;

    public interface IJobScheduler {
        void Schedule(IScheduler scheduler);
    }
}