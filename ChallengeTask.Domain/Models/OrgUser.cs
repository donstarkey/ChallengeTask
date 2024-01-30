namespace TaskAPI.Models
{
    public record OrgUser : ChallengeBase
    {
        public OrgUser(int id, DateTimeOffset createdat, string name, int organizationId, string avatar)
            : base(id, createdat, name)
        {
            this.organizationId = organizationId;
            this.avatar = avatar;
        }

        private int organizationId;
        private string avatar;
        public int OrganizationId
        {
            get => organizationId;
            set
            {
                organizationId = value;
            }
        }
        public string Avatar
        {
            get => avatar;
            set
            {
                avatar = value;
            }
        }

    }
}
