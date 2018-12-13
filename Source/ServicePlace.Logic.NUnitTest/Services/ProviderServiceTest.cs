using NUnit.Framework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Services;
using ServicePlace.Model.ViewModels;
using System.Linq;

namespace ServicePlace.Logic.NUnitTest.Services
{
    [TestFixture]
    public class ProviderServiceTest
    {
        [Test]
        public void SearchProvider_FoundByTitleOrDescriptionProvider()
        {
            var applicationContext = new ApplicationContext();
            var providerRepository = new ProviderRepository(applicationContext);
            var providerService = new ProviderService(providerRepository, null, null, null);
            var expected = providerService.Get(1);
            var searchQuery = "provider";
            var actual = providerService.SearchProvider(searchQuery).First();
            Assert.AreEqual(expected.Id, actual.Id);
        }
    }
}
