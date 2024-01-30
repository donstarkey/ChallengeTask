using ChallengeTask.Infrastructure.Data.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Models;


namespace Infrastructure.Data.Repositories
{

    public class ApiOrganizationRepository<T> : IRepository<T> where T : Organization
    {
        public static HashSet<List<Organization>> OrgsHashSet = new HashSet<List<Organization>>();
        private Organization o = new Organization(-1,DateTimeOffset.Now, "ERROR");

        public Task AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            string jsonorgs;
            string url = "https://607a0575bd56a60017ba2618.mockapi.io/organization/"; // sample url
            using (HttpClient client = new HttpClient())
            {
                jsonorgs = await client.GetStringAsync(url);
            }

            //two different ways to deserialize the json string into List<Organization>
            var orgObj = System.Text.Json.JsonSerializer.Deserialize<List<Organization>>(jsonorgs);
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
