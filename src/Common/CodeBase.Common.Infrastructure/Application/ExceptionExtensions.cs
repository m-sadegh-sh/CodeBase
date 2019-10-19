namespace CodeBase.Common.Infrastructure.Application {
    using System;
    using System.Text;

    public static class ExceptionExtensions {
        public static string ToFriendlyString(this Exception exception) {
            var result = new StringBuilder();

            result.AppendLine("Message: " + exception.Message);
            result.AppendLine("Source: " + exception.Source);
            result.AppendLine("TargetSite: " + exception.TargetSite);
            result.AppendLine("StackTrace: " + exception.StackTrace);

            return result.ToString();
        }
    }
}