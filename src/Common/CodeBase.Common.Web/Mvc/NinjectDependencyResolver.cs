namespace CodeBase.Common.Web.Mvc {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Ninject;

    public sealed class NinjectDependencyResolver : IDependencyResolver {
        private readonly IKernel _container;

        public NinjectDependencyResolver(IKernel container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType == null)
                return null;
            try {
                return serviceType.IsAbstract || serviceType.IsInterface
                           ? _container.TryGet(serviceType)
                           : _container.Get(serviceType);
            } catch {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAll(serviceType);
        }
    }
}