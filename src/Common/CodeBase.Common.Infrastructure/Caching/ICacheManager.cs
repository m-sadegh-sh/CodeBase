namespace CodeBase.Common.Infrastructure.Caching {
    public interface ICacheManager {
        void Set(string key,
                 object data,
                 int cacheTime);

        bool IsSet(string key);
        T Get<T>(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}