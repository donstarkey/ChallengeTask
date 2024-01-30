namespace TaskAPI.Models
{
    public record OrgUserPhone : ChallengeBase
    {
        public OrgUserPhone(int id, DateTimeOffset createdat, string name)
            : base(id, createdat, name)
        {
        }

        private int userId;
        private bool blacklisted;
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
            }
        }
        public bool Blacklisted
        {
            get => blacklisted;
            set
            {
                blacklisted = value;
            }
        }

    }
}
