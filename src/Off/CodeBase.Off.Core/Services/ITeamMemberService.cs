namespace CodeBase.Off.Core.Services {
    using System.Collections.Generic;

    using CodeBase.Off.Core.Domain;

    public interface ITeamMemberService {
        bool Exists(int userId);
        TeamMember Get(int userId);
        List<TeamMember> GetList();
        void Save(TeamMember member);
        void Delete(int userId);
    }
}