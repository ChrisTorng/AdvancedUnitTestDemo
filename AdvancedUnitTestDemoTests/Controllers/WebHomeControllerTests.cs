using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdvancedUnitTestDemo;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedUnitTestDemoTests.Controllers
{
    [TestClass]
    public class WebHomeControllerTests
    {
        [TestMethod]
        public async Task HomeController_Empty_Tests()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(new Uri("/", UriKind.Relative));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(content, "Alonso");
            StringAssert.Contains(content, "Justice");
        }

        [TestMethod]
        public async Task HomeController_Get_OrderByDateDesc_SearchA_Tests()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(new Uri("/?sortOrder=date&searchString=a", UriKind.Relative));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(content, "Alonso");
            Assert.IsFalse(content.Contains("Justice", StringComparison.Ordinal));
        }

        [TestMethod]
        public async Task HomeController_Post_OrderByDateDesc_SearchA_Tests()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var response = await client.PostAsJsonAsync(
                new Uri("/Home/Index2", UriKind.Relative),
#pragma warning disable IDE0050 // Convert to tuple
                new { SortOrder = "date", SearchString = "a" });
#pragma warning restore IDE0050 // Convert to tuple
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(content, "Alonso");
            Assert.IsFalse(content.Contains("Justice", StringComparison.Ordinal));
        }
    }
}
