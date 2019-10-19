namespace CodeBase.Common.Infrastructure.Application {
    using System.Collections.Generic;

    public static class DictionaryExstensions {
        public static void TryToAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value, bool replaceIfExists = false) {
            if (dictionary.ContainsKey(key)) {
                if (replaceIfExists)
                    dictionary[key] = value;
            } else
                dictionary.Add(key, value);
        }
    }
}