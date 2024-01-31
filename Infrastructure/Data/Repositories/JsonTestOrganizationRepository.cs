using ChallengeTask.Infrastructure.Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskAPI.Models;
//using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Repositories
{
    public class JsonTestOrganizationRepository<T> : IRepository<T>
        where T : Organization
    {
        public static HashSet<List<Organization>> OrgsHashSet = new HashSet<List<Organization>>();

        public JsonTestOrganizationRepository()
        {
            
        }
        public Task AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            var currentDir = Environment.CurrentDirectory;
            var relPath1 = @"Json\SampleOrgs.json";
            var newPath = Path.GetFullPath(Path.Combine(currentDir, relPath1));

            using StreamReader streamReader = new(newPath);
            var jsonorgs = streamReader.ReadToEnd();
            var orgList = JsonConvert.DeserializeObject<List<Organization>>(jsonorgs);

            if (orgList is not null && orgList.Count() > 0)
            {
                OrgsHashSet.Add(orgList);
                return (IEnumerable<T>)orgList;

            }
            else
            {
                return (IEnumerable<T>)orgList;

            }
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
