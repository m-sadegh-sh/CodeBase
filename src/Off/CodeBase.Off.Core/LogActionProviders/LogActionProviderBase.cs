namespace CodeBase.Off.Core.LogActionProviders {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public abstract class LogActionProviderBase<T> {
        public LogAction Build(T old, T @new) {
            var action = new LogAction {
                AffectedType = typeof (T)
            };

            BuildCore(old, @new, action);

            return action;
        }

        protected abstract void BuildCore(T old, T @new, LogAction action);

        protected LogActionProviderContext<T> WithInstance(IDictionary<string, ValueSnapshot> snapshots, T old, T @new) {
            var context = new LogActionProviderContext<T>(snapshots, old, @new);

            return context;
        }
    }
}