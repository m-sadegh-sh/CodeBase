namespace CodeBase.Common.Infrastructure.Caching {
    using System;
    using System.Linq;
    using System.Runtime.Caching;

    public class MemoryCacheManager : ICacheManager {
        protected ObjectCache Cache {
            get { return MemoryCache.Default; }
        }

        public void Set(string key,
                        object value,
                        int duration) {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            key = Escape(key);

            if (value == null)
                return;

            var policy = new CacheItemPolicy {
                    AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(duration)
            };

            Cache.Add(new CacheItem(key,
                                    value),
                      policy);
        }

        public bool IsSet(string key) {
            if (string.IsNullOrEmpty(key))
                return false;

            key = Escape(key);

            var isSet = Cache.Contains(key);

            return isSet;
        }

        public T Get<T>(string key) {
            if (string.IsNullOrEmpty(key))
                return default(T);

            key = Escape(key);

            var result = (T) Cache[key];

            return result;
        }

        public void Remove(string key) {
            if (string.IsNullOrEmpty(key))
                return;

            key = Escape(key);

            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern) {
            if (string.IsNullOrEmpty(pattern))
                return;

            pattern = Escape(pattern);

            try {
                //var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

                var keysToRemove = Cache.Where(kvp => kvp.Key.StartsWith(pattern)).
                                         Select(kvp => kvp.Key).
                                         ToList();

                foreach (var key in keysToRemove)
                    Remove(key);
            } catch {}
        }

        public void Clear() {
            foreach (var kvp in Cache)
                Remove(kvp.Key);
        }

        private static string Escape(string key) {
            //key = key.Replace(@":", @"COLON");
            //key = key.Replace(@"\\", @"-");
            //key = key.Replace(@".", @"\.");

            return key;
        }
    }
}