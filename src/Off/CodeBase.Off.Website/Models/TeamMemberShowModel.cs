namespace CodeBase.Off.Website.Models {
    public sealed class TeamMemberShowModel {
        public UserSummaryModel User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName {
            get { return (FirstName + " " + LastName).Trim(); }
        }

        public string PublicEmail { get; set; }
        public string Position { get; set; }
        public string Biography { get; set; }
    }
}