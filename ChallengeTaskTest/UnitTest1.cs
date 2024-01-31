using ChallengeTask.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskAPI.Controllers;
using TaskAPI.Models;

namespace ChallengeTaskTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        public class OrganizationControllerTest
        {
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IConfiguration _configuration;
            private readonly ILogger<OrganizationController> _logger;
            private readonly IRepository<Organization> _repository;

            [Test]
            public void TestDetailsView()
            {
                var controller = new OrganizationController (_logger, _webHostEnvironment,_configuration, _repository);
                var result = controller.GetAllOrganizations();
                Assert.IsNotNull(result);
                Assert.IsTrue(10 == result.Result.Count());

            }
        }
    }
}