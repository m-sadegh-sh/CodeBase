namespace CodeBase.Off.Website.Jobs {
    using System;
    using System.Linq;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Services;

    using Quartz;

    public sealed class NotConfirmedSubscriptionsRemoverJob : IJob {
        public const string Identity = "PENDING_SUBSCRIPTINS_RMOVER";

        public void Execute(IJobExecutionContext context) {
            var subscriptionService = IoC.Get<ISubscriptionService>();

            var subscriptions = subscriptionService.GetList().
                                                    Where(s => s.IsConfirmationSent && !s.IsConfirmed && s.SubscribDateUtc <= DateTime.UtcNow.AddDays(3));

            foreach (var subscription in subscriptions)
                subscriptionService.Delete(subscription.Email);
        }
    }
}