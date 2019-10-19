namespace CodeBase.Off.Core.Services.Impl {
    using System;

    using CodeBase.Off.Core.Domain;

    public static class HitInfoServiceExtensions {
        public static bool Resolve(this IHitInfoService service,
                                   int hitId,
                                   string requestIp = null,
                                   string referrerUrl = null,
                                   DateTime? hitDate = null) {
            var isResolvable = service.IsResolvable(hitId,
                                                    requestIp);

            if (isResolvable) {
                var info = new HitInfo {
                        HitId = hitId,
                        RequestIp = requestIp,
                        ReferrerUrl = referrerUrl,
                        HitDateUtc = hitDate ?? DateTime.UtcNow
                };

                service.Save(info);
            }

            return isResolvable;
        }
    }
}