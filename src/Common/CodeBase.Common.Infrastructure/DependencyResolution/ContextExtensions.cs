namespace CodeBase.Common.Infrastructure.DependencyResolution {
    using Ninject.Activation;

    public static class ContextExtensions {
        public static TService GetService<TService>(this IContext context) {
            var service = context.Kernel.GetService(typeof (TService));

            return service != null ? (TService) service : default(TService);
        }
    }
}