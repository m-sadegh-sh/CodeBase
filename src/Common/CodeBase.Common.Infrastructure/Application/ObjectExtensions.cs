namespace CodeBase.Common.Infrastructure.Application {
    public static class ObjectExtensions {
        public static string ToStringOrNull(this object value) {
            if (value == null)
                return null;

            return value.ToString();
        }

        public static string ToStringOrEmpty(this object value) {
            if (value == null)
                return string.Empty;

            return value.ToString();
        }
    }
}