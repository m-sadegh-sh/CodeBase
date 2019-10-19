namespace CodeBase.Off.Website.Jobs {
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Infrastructure.Storage.Json;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Utilities;

    using Quartz;

    public sealed class CloudBackupJob : IJob {
        public const string Identity = "CLOUD_BACKUP";

        public void Execute(IJobExecutionContext context) {
            var cloudService = IoC.Get<ICloudService>();
            var pathProvider = IoC.Get<JsonPathProvider>();

            var archiveFileName = cloudService.ArchiveFolder(pathProvider.BasePath);

            context.Result = archiveFileName;
        }
    }
}