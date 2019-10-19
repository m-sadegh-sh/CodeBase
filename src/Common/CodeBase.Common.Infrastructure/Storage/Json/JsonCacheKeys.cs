namespace CodeBase.Common.Infrastructure.Storage.Json {
    public static class JsonCacheKeys {
        public const string Pattern = "typename:{0}.";
        public const string Exists = Pattern + "{1}:exists";
        public const string Value = Pattern + "{1}:value";
    }
}