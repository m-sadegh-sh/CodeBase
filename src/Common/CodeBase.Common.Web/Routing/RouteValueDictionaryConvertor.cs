namespace CodeBase.Common.Web.Routing {
    using System.Collections.Generic;
    using System.Web.Routing;

    public static class RouteValueDictionaryConvertor {
        public static RouteValueDictionary Convert(object routeValues) {
            if (routeValues == null)
                return new RouteValueDictionary();

            var routeValueDictionary = routeValues as RouteValueDictionary;
            if (routeValueDictionary != null)
                return routeValueDictionary;

            var dictionary = routeValues as IDictionary<string, object>;
            if (dictionary != null)
                return new RouteValueDictionary(dictionary);

            return new RouteValueDictionary(routeValues);
        }

        public static RouteValueDictionary MergeWith(this RouteValueDictionary first, RouteValueDictionary second, bool updateDuplicates = true) {
            foreach (var item in second) {
                if (first.ContainsKey(item.Key)) {
                    if (updateDuplicates)
                        first[item.Key] = item.Value;
                } else
                    first.Add(item.Key, item.Value);
            }

            return first;
        }
    }
}