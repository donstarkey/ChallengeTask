namespace TaskAPI.Models
{
    //public record OrganizationSummary : ChallengeBase //TODO: would like to inherit here, but was causing json to appear out of ord
    public record OrganizationSummary 
    {
        private List<OrgUser> users = new List<OrgUser>();
        private int totalCount;
        private int blacklistTotal;
        private string name;
        private int id;

        //public OrganizationSummary(int id, DateTimeOffset createdat, string name)
        //    : base(id, createdat, name)
        //{
        //}
        public OrganizationSummary()
        {
            
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }



        public int TotalCount
        {
            get => totalCount;
            set
            {
                totalCount = value;
            }
        }

        public int BlacklistTotal
        {
            get => blacklistTotal;
            set
            {
                blacklistTotal = value;
            }
        }

        public List<OrgUser> Users
        {
            get => users;
            set
            {
                users = value;
                //OnPropertyChanged(nameof(Users)); //TODO: Will put in code that will clear cache on this event
            }
        }

    }
}
