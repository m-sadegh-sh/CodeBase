namespace CodeBase.Common.Infrastructure.Caching {
    using System;

    public static class CacheManagerExtensions {
        public static T Get<T>(this ICacheManager cacheManager,
                               string key,
                               Func<T> acquire) {
            return Get(cacheManager,
                       key,
                       60,
                       acquire);
        }

        public static T Get<T>(this ICacheManager cacheManager,
                               string key,
                               int duration,
                               Func<T> acquire) {
            //if (cacheManager.IsSet(key))
            //    return cacheManager.Get<T>(key);

            var value = acquire();

            //cacheManager.Set(key, value, duration);

            return value;
        }
    }
}