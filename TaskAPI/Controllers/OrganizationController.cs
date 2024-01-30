using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskAPI.Models;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Collections;
using ChallengeTask.Infrastructure.Data.Repositories;
//using System.Web.Script.Serialization;

namespace TaskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrganizationController> _logger;
        private readonly IRepository<Organization> _repository;
        public static HashSet<List<Organization>> OrgsHashSet = new HashSet<List<Organization>>();


        public OrganizationController(ILogger<OrganizationController> logger, IWebHostEnvironment webHostEnvironment, 
            IConfiguration configuration, IRepository<Organization> repo)
        {
            _repository = repo;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }


        public async Task<List<Organization>> GetAllOrganizations()
        {
            //TODO: I implemented the repository pattern for organizations now we can easily substitute different data stores in application
            //TODO: Still need to implement the other repositories, IE OrganiationSummay, OrgUser and OrgUserPhone. 
            // this is a POC for further application development.  Do as time allows.
            var orgList = await  _repository.AllAsync();
            return (List<Organization>)orgList;

        }

        
        [HttpGet]

        //public async Task<List<OrganizationSummary>> GetOrganizationSummary() //NOTE: This signature also returns the proper json simply uncomment line 94 for proper r 
        public async Task<JsonResult> GetOrganizationSummary()

        {
            int batchSize = 10;
            List<OrganizationSummary> orgSummaryList = new List<OrganizationSummary>();

            //NOTE: the following call is using the Repository Pattern. Just wanted to demonstrate my knowledge of this concept.
            var organizations = await GetAllOrganizations();

            int totalOrgCount = organizations.Count;

            int numberOfBatches = totalOrgCount / batchSize;
            int remainingOrgBatch = totalOrgCount % batchSize; //If equal to zero batch size identified each org, otherwise process this last few orgs
            int delayBatchProcessing = 8000;

            List<OrganizationSummary> orgSummaryBatch = new List<OrganizationSummary>();
            //TODO: Introduce a Worker Thread to improve response time for the data intensive methods.

            for (int i = 0; i<batchSize; i++) 
            {
                orgSummaryBatch = await GetOrgUserBatch(organizations.Skip(i*batchSize).Take(batchSize).ToArray());
                delayBatchProcessing = i == 2 || i == 4 ? delayBatchProcessing + 2000 : delayBatchProcessing;
                Thread.Sleep(delayBatchProcessing);
                orgSummaryList.AddRange(orgSummaryBatch);
            //if (i == 1) break; //This was in place for testing purposes. to limit results and therefore time of request
            }

            if (remainingOrgBatch > 0) //process any remaining orgs not accounted for in batch calcualation
            {
                orgSummaryBatch = await GetOrgUserBatch(organizations.Skip(numberOfBatches * batchSize).Take(remainingOrgBatch).ToArray());
                Thread.Sleep(delayBatchProcessing);
                orgSummaryList.AddRange(orgSummaryBatch);
            }

            //two different ways to deserialize the json string into List<Organization>
            //           var orgObj = System.Text.Json.JsonSerializer.Deserialize<List<Organization>>(jsonorgs);
            //           var orgList = JsonConvert.DeserializeObject<List<Organization>>(jsonorgs);
            
            
            //return orgSummaryList;
            return new JsonResult(orgSummaryList);

        }


        private static async Task<List<OrganizationSummary>> GetOrgUserBatch(Organization[] ids)
        {
            var users = await GetOrgUsersById(ids);

             return users;
        }

        private static async Task<List<OrganizationSummary>> GetOrgUsersById(Organization[] ids)
        {
            StringBuilder allUsers = new StringBuilder();
            List<OrganizationSummary> items = new List<OrganizationSummary>();
            //allUsers.Append("["); //Identify this string as a json array
            string jsonorgs;
            //for (int i = startingIdx; i <= endingIdx; i++)
            foreach (Organization org in ids)
            {
                OrganizationSummary item = new OrganizationSummary();

                string url = $"https://607a0575bd56a60017ba2618.mockapi.io/users/{org.Id}"; // sample url
                using (HttpClient client = new HttpClient())
                {
                    jsonorgs = await client.GetStringAsync(url);
                }

                string orgusers = "["+jsonorgs+"]";
                var userList = JsonConvert.DeserializeObject<List<OrgUser>>(orgusers);  
                //build OrgSummary

                item.Name = org.Name;
                item.Id = org.Id;
                int blacklistTotal = 0;

                foreach (var user in userList) //get phones
                {
                    var phones = await GetUsersPhones(user.Id, user.Id);

                    item.TotalCount = phones.Count;
                    if(phones.Count == 0)
                    {
                        user.Name += ", No PHONE Found";
                    }

                    blacklistTotal = phones.Count(x => x.Blacklisted == true);

                }

                item.BlacklistTotal = blacklistTotal;

                item.Users = userList;

                items.Add(item);
                allUsers.Append(jsonorgs + ",");
            }
            var currentSummary = items;

            return currentSummary;
        }


        public static async Task<List<OrgUserPhone>> GetUsersPhones(int userId, int phoneId)
        {
            Thread.Sleep(1500);

            string jsonorgs;
            var errorPhoneList = new List<OrgUserPhone>();  //TODO:  Enhance app by saving this list to db

            string url = $"https://607a0575bd56a60017ba2618.mockapi.io/organization/{userId}/users/{phoneId}/phones"; // sample url
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    jsonorgs = await client.GetStringAsync(url);

                }
                catch (Exception e)
                {
                    var notFound = new OrgUserPhone(-1, DateTimeOffset.UtcNow, $"Not Found for PhoneId {phoneId}")
                    {
                        UserId = userId,
                        Blacklisted = false ,
                    };

                    //errorPhoneList.Add(notFound); //instead log a not found phone //TODO:  Enhance app by saving this list to db
                    return errorPhoneList;
                }            
            }

            var userPhoneList = JsonConvert.DeserializeObject<List<OrgUserPhone>>(jsonorgs);

            if (userPhoneList != null)
            {
                return userPhoneList;
            }
            else
            {
                throw new Exception("Null Exception");
            }
        }
        
        
    }
}
