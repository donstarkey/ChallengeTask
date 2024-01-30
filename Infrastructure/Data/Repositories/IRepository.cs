namespace ChallengeTask.Infrastructure.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetAsync();

        Task<T> FindByAsync(string value);
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> AllOrganizationsAsync();

        Task AddAsync(T item);

        Task SaveChangesAsync();
    }
}
