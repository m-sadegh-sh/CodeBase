namespace CodeBase.Off.Core.Services.Impl {
    using System.Security.Principal;
    using System.Web.Security;

    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Off.Core.Domain;

    public static class UserServiceExtensions {
        public static User GetCurrent(this IUserService service) {
            var identity = IoC.Get<IIdentity>();
            var configService = IoC.Get<IConfigService>();

            if (!VerifyIdentity(identity)) {
                return new User {
                        Id = -1
                };
            }

            User current;

            var formsIdentity = identity as FormsIdentity;
            var friendlyName = formsIdentity != null ? formsIdentity.Ticket.UserData : identity.Name;

            if (string.IsNullOrEmpty(friendlyName))
                friendlyName = identity.Name;

            var isAdmin = identity.IsAuthenticated && configService.Current.IsAdmin(identity.Name);

            var dbUser = service.Get(identity.Name);

            if (dbUser != null)
                current = dbUser;
            else {
                var user = new User {
                        UserName = identity.Name,
                        FriendlyName = friendlyName,
                        IsAuthenticated = identity.IsAuthenticated,
                        IsAdmin = isAdmin
                };

                service.Save(user);

                current = user;
            }

            return current;
        }

        private static bool VerifyIdentity(IIdentity identity) {
            return !(identity == null || identity is WindowsIdentity || !identity.IsAuthenticated);
        }
    }
}