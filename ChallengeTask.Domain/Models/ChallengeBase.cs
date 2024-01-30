using Microsoft.VisualBasic;

namespace TaskAPI.Models
{
    public abstract record ChallengeBase
    {
        private  int id;
        private  string name = string.Empty;
        private  DateTimeOffset createdat;

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
            }
        }

        public DateTimeOffset CreatedAt
        {
            get => createdat;
            set
            {
                createdat = value;
            }
        }
        public ChallengeBase(int id, DateTimeOffset createdat,string name) { 
            this.id = id;
            this.createdat = createdat;
            this.name = name;      
        }
    }
}
