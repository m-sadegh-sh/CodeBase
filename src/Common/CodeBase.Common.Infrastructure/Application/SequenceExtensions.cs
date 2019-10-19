namespace CodeBase.Common.Infrastructure.Application {
    using System.Collections.Generic;
    using System.Linq;

    public static class SequenceExtensions {
        public static bool IsEmpty<T>(this IEnumerable<T> items) {
            return items == null || items.Any();
        }

        public static bool IsEmpty<T>(this ICollection<T> items) {
            return items == null || items.Count == 0;
        }

        public static bool IsLast<T>(this ICollection<T> source, T item) where T : class {
            var last = source.LastOrDefault();

            return last != null && last.Equals(item);
        }
    }
}