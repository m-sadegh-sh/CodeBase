namespace CodeBase.Off.Core.LogActionProviders {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Off.Core.Domain;

    public sealed class LogActionProviderContext<T> {
        private readonly IDictionary<string, ValueSnapshot> _snapshots;
        private readonly T _old;
        private readonly T _new;

        public LogActionProviderContext(IDictionary<string, ValueSnapshot> snapshots, T old, T @new) {
            _snapshots = snapshots;
            _old = old;
            _new = @new;
        }

        public LogActionProviderContext<T> AddIfChanged(Expression<Func<T, object>> property) {
            var compiledProperty = property.Compile();

            var oldValue = compiledProperty(_old);
            var newValue = compiledProperty(_new);

            if (Equals(oldValue, newValue)) {
                _snapshots.Add(property.GetMemberName(), new ValueSnapshot {
                    OldValue = oldValue,
                    NewValue = newValue
                });
            }

            return this;
        }
    }
}