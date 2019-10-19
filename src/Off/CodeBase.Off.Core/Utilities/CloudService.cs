namespace CodeBase.Off.Core.Utilities {
    using System;
    using System.IO;

    using AppLimit.CloudComputing.SharpBox;
    using AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox;

    using CodeBase.Off.Core.Domain;
    using CodeBase.Off.Core.Services;

    using Ionic.Zip;

    using MVC.Utilities.Routing;

    public sealed class CloudService : ICloudService {
        private readonly DropBoxConfiguration _storageConfiguration;
        private readonly ICloudStorageAccessToken _accessToken;
        private readonly Config _config;

        public CloudService(Config config) {
            _config = config;

            _storageConfiguration = DropBoxConfiguration.GetStandardConfiguration();

            var requestToken = DropBoxStorageProviderTools.GetDropBoxRequestToken(_storageConfiguration,
                                                                                  _config.Cloud.ConsumerKey,
                                                                                  _config.Cloud.ConsumerSecret);

            _accessToken = DropBoxStorageProviderTools.ExchangeDropBoxRequestTokenIntoAccessToken(_storageConfiguration,
                                                                                                  _config.Cloud.ConsumerKey,
                                                                                                  _config.Cloud.ConsumerSecret,
                                                                                                  requestToken);
        }

        public string ArchiveFolder(string folderPath) {
            var backupName = Path.GetFileName(folderPath).ToSlug();
            var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var archiveFilename = string.Format("{0}-{1}.zip", backupName, timestamp);

            using (var zip = new ZipFile()) {
                using (var zipMemoryStream = new MemoryStream()) {
                    zip.AddDirectory(folderPath);
                    zip.Save(zipMemoryStream);
                    Save(archiveFilename, zipMemoryStream);
                }
            }

            return archiveFilename;
        }

        private void Save(string fileName, MemoryStream stream) {
            var storage = new CloudStorage();

            storage.Open(_storageConfiguration, _accessToken);

            var cloudFolder = storage.GetFolder(_config.Cloud.BackupFolder);

            if (cloudFolder == null)
                throw new Exception("Cloud folder not found: " + _config.Cloud.BackupFolder);

            var cloudFile = storage.CreateFile(cloudFolder, fileName);

            using (var cloudStream = cloudFile.GetDataTransferAccessor().GetUploadStream(stream.Position))
                cloudStream.Write(stream.GetBuffer(), 0, (int) stream.Position);

            if (storage.IsOpened)
                storage.Close();
        }
    }
}