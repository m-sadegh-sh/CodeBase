namespace CodeBase.Off.Core.Domain {
    public sealed class TeamMember {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PublicEmail { get; set; }
        public string Position { get; set; }
        public string Biography { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int ViewCount { get; set; }
    }
}