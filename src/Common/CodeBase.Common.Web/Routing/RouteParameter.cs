namespace CodeBase.Common.Web.Routing {
    using System.Web.Routing;

    public static class RouteParameter {
        public static RouteValueDictionary Add<T>(string key, T value) {
            return Append(null, key, value);
        }

        public static RouteValueDictionary Append<T>(object routeValues, string key, T value) {
            return Append(RouteValueDictionaryConvertor.Convert(routeValues), key, value);
        }

        public static RouteValueDictionary Append<T>(this RouteValueDictionary routeValues, string key, T value) {
            if (routeValues == null)
                routeValues = new RouteValueDictionary();

            if (routeValues.ContainsKey(key))
                routeValues[key] = value;
            else
                routeValues.Add(key, value);

            return routeValues;
        }
    }
}