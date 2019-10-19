namespace CodeBase.Common.Infrastructure.DependencyResolution {
    using Microsoft.Practices.ServiceLocation;

    public static class IoC {
        public static TService Get<TService>() {
            try {
                return ServiceLocator.Current.GetInstance<TService>();
            } catch {
                return default(TService);
            }
        }
    }
}