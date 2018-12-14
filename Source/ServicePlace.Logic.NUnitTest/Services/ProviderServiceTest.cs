using NUnit.Framework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Services;
using ServicePlace.Model.ViewModels;
using System.Linq;
using ServicePlace.DataInitializer;

namespace ServicePlace.Logic.NUnitTest.Services
{
    [TestFixture]
    public class ProviderServiceTest
    {
        private Initializer _initializer;

        [SetUp]
        public void Init()
        {
            _initializer = new Initializer(10);
            _initializer.InitializeDb();
        }

        [TearDown]
        public void Dispose()
        {
            _initializer.ClearDb();
        }

        [Test]
        public void SearchProvider_FoundByTitleOrDescriptionProvider()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Title).Last();
            var actual = _initializer.ProviderService.SearchProvider(expected.Title).First();
            Assert.AreEqual(expected, actual);
        }
    }
}
