using ChallengeTask.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Models;

namespace Infrastructure.Data.Repositories
{
    public class JsonTestOrganizationRepository<T> : IRepository<T>
        where T : Organization
    {
        public Task AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AllOrganizationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
