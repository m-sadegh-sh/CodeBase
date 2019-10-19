namespace CodeBase.Off.Website.Controllers {
    using System;
    using System.Web.Mvc;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Web.Extensions;
    using CodeBase.Off.Core.Services;
    using CodeBase.Off.Core.Services.Impl;

    public sealed partial class StatisticsController : ControllerBase
    {
        private readonly IResourceHitService _hitService;

        public StatisticsController(IResourceHitService hitService) {
            _hitService = hitService;
        }

        public virtual EmptyResult Apply(Guid sessionId,
                                 string url) {
            _hitService.Resolve(url,
                                Request.RemoteIp(),
                                Request.UrlReferrer.ToStringOrNull());

            return Empty();
        }
    }
}