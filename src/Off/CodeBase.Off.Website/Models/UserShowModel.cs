namespace CodeBase.Off.Website.Models {
    public sealed class UserShowModel {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
        public string JoinDate { get; set; }
        public string PrettyJoinDate { get; set; }
        public string LastActivityDate { get; set; }
        public string PrettyLastActivityDate { get; set; }
        public int EntriesCount { get; set; }
    }
}