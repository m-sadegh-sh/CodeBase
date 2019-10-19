namespace CodeBase.Common.Infrastructure.Storage.Json {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class JsonKeyProvider {
        private readonly IDictionary<Type, List<Func<object, object>>> _keys;

        public JsonKeyProvider() {
            _keys = new ConcurrentDictionary<Type, List<Func<object, object>>>();
        }

        public void RegisterKey<T>(params Func<T, object>[] keys) {
            var funcs = new List<Func<object, object>>();

            foreach (var key in keys) {
                var keyShadow = key;

                funcs.Add(e => keyShadow((T) e));
            }

            _keys.Add(typeof (T), funcs);
        }

        public List<string> GetAllKeyValues<T>(T entity) {
            if (!_keys.ContainsKey(typeof (T)))
                throw new EntityNotFoundException<T>();

            var list = new List<string>();

            var ownedKey = _keys.Single(k => k.Key == typeof (T));

            foreach (var result in ownedKey.Value)
                list.Add(string.Join("", result(entity)));

            return list;
        }
    }
}