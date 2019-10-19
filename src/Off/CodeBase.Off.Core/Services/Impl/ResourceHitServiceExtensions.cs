namespace CodeBase.Off.Core.Services.Impl {
    using System;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;

    public static class ResourceHitServiceExtensions {
        public static bool Resolve(this IResourceHitService service,
                                   string url,
                                   string requestIp = null,
                                   string referrerUrl = null,
                                   DateTime? hitDate = null) {
            var exists = service.Exists(url);

            ResourceHit hit;

            if (exists)
                hit = service.Get(url);
            else {
                hit = new ResourceHit {
                        Url = url
                };

                service.Save(hit);
            }

            var hitInfoService = IoC.Get<IHitInfoService>();

            return hitInfoService.Resolve(hit.Id,
                                          requestIp,
                                          referrerUrl,
                                          hitDate);
        }
    }
}