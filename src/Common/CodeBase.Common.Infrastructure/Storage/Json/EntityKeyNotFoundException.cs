namespace CodeBase.Common.Infrastructure.Storage.Json {
    using System;

    public sealed class EntityNotFoundException<TEntity> : Exception {
        public EntityNotFoundException() : this(string.Format("No key was registered for type '{0}'.", typeof (TEntity))) {}

        public EntityNotFoundException(string message) : this(message, null) {}

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) {}
    }
}