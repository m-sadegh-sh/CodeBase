namespace CodeBase.Off.Core.Services {
    using CodeBase.Off.Core.Domain;

    public interface IConfigService {
        Config Current { get; }
        void Save();
    }
}