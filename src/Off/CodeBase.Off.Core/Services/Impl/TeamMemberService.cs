namespace CodeBase.Off.Core.Services.Impl {
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Common.Infrastructure.Storage;
    using CodeBase.Off.Core.Domain;

    public sealed class TeamMemberService : ITeamMemberService {
        private readonly IRepository _repository;

        public TeamMemberService(IRepository repository) {
            _repository = repository;
        }

        public bool Exists(int userId) {
            if (userId < 1)
                return false;

            return _repository.Exists<TeamMember>(userId);
        }

        public TeamMember Get(int userId) {
            if (userId < 1)
                return null;

            return _repository.Single<TeamMember>(userId);
        }

        public List<TeamMember> GetList() {
            return _repository.All<TeamMember>().ToList();
        }

        public void Save(TeamMember member) {
            _repository.Save(member);
        }

        public void Delete(int userId) {
            if (userId < 1)
                return;

            _repository.Delete<TeamMember>(userId);
        }
    }
}