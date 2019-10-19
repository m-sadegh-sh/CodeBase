namespace CodeBase.Off.Website.Configurators {
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Website.Jobs;

    using Quartz;

    public static class JobRunner {
        public static void StartJobs() {
            var scheduler = IoC.Get<IScheduler>();

            new CloudBackupScheduler().Schedule(scheduler);
            new EntriesSenderScheduler().Schedule(scheduler);

            scheduler.Start();
        }
    }
}