namespace TaskAPI.Models
{
    public record Organization : ChallengeBase
    {
        public Organization(int id, DateTimeOffset createdat, string name) 
            : base(id, createdat, name)
        {
        }
    }
}
