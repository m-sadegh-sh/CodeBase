namespace CodeBase.Off.Core.Services.Impl {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Security;

    using CodeBase.Common.Infrastructure.Application;
    using CodeBase.Common.Infrastructure.DependencyResolution;
    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class UserService : IUserService {
        private readonly IConfigService _configService;
        private readonly IRepository _repository;
        private User _current;

        public UserService(IConfigService configService, IRepository repository) {
            _configService = configService;
            _repository = repository;
        }

        public User Current {
            get { return _current ?? (_current = GetCurrent()); }
        }

        private User GetCurrent() {
            var identity = IoC.Get<IIdentity>();

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

            var isAdmin =
                identity.IsAuthenticated
                && _configService.Current.IsAdmin(identity.Name);

            var dbUser = Get(identity.Name);

            if (dbUser != null)
                current = dbUser;
            else {
                var user = new User {
                    UserName = identity.Name,
                    FriendlyName = friendlyName
                };

                Save(user);

                current = user;
            }

            return current;
        }

        private static bool VerifyIdentity(IIdentity identity) {
            return !(identity == null || identity is WindowsIdentity || !identity.IsAuthenticated);
        }

        public bool Exists(int id) {
            if (id < 1)
                return false;

            return _repository.Exists<User>(id);
        }

        public bool Exists(string userName) {
            if (userName.AsNullIfEmpty() == null)
                return false;

            return GetList().Any(u => u.UserName == userName);
        }

        public User Get(int id) {
            if (id < 1)
                return null;

            if (!Exists(id))
                return null;

            return _repository.Single<User>(id);
        }

        public User Get(string userName) {
            if (userName.AsNullIfEmpty() == null)
                return null;

            if (!Exists(userName))
                return null;

            return GetList().First(u => u.UserName == userName);
        }

        public List<User> GetList() {
            return _repository.All<User>().ToList();
        }

        public int Save(User user) {
            if (user.JoinDateUtc == default(DateTime)) {
                var isUpdate = _repository.Exists<User>(user.UserName);

                if (isUpdate) {
                    var oldUser = _repository.Single<User>(user.UserName);
                    user.Id = oldUser.Id;
                    user.JoinDateUtc = oldUser.JoinDateUtc;
                } else {
                    user.Id = DateTime.UtcNow.Timestamp();
                    user.JoinDateUtc = DateTime.UtcNow;
                }
            }

            _repository.Save(user);

            return user.Id;
        }

        public void Delete(int id) {
            if (id < 1)
                return;

            if (!Exists(id))
                return;

            _repository.Delete<User>(id);
        }

        public void Delete(string userName) {
            if (userName.AsNullIfEmpty() == null)
                return;

            if (!Exists(userName))
                return;

            var user = Get(userName);

            _repository.Delete<User>(user.Id);
        }
    }
}