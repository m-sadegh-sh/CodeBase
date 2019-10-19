namespace CodeBase.Common.Web.Optimization.Bundling {
    using System;

    using dotless.Core.Loggers;

    public class LessLogger : ILogger {
        public void Log(LogLevel level, string message) {
            throw new Exception("Level: " + level + Environment.NewLine + message);
        }

        public void Info(string message) {
            throw new Exception(message);
        }

        public void Info(string message, params object[] args) {
            throw new Exception(string.Format(message, args));
        }

        public void Debug(string message) {
            throw new Exception(message);
        }

        public void Debug(string message, params object[] args) {
            throw new Exception(string.Format(message, args));
        }

        public void Warn(string message) {
            throw new Exception(message);
        }

        public void Warn(string message, params object[] args) {
            throw new Exception(string.Format(message, args));
        }

        public void Error(string message) {
            throw new Exception(message);
        }

        public void Error(string message, params object[] args) {
            throw new Exception(string.Format(message, args));
        }
    }
}