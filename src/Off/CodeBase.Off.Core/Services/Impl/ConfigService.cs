namespace CodeBase.Off.Core.Services.Impl {
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class ConfigService : IConfigService {
        private const string CURRENT_KEY = "Current";

        private readonly ILogService _logService;
        private readonly IRepository _repository;

        public ConfigService(ILogService logService, IRepository repository) {
            _logService = logService;
            _repository = repository;

            Current = _repository.Single<Config>(CURRENT_KEY);

            if (Current == null) {
                _logService.Warning("Config couldn't be found!");

                Current = BuildFallback();
                Save();
                _logService.Info("Fallback Config is built and persisted.");
            }
        }

        public Config Current { get; private set; }

        public void Save() {
            _repository.Save(Current);
        }

        private static Config BuildFallback() {
            var config = new Config();

            return config;
        }
    }
}