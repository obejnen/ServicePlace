using NUnit.Framework;
using System.Linq;
using ServicePlace.DataInitializer;
using ServicePlace.Model.DataModels;

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
        public void Create_CreatedProvider_ThrowsNoExceptions()
        {
            var expected = new Provider
            {
                Approved = false,
                Body = "Provider body",
                Title = "Provider title",
                Price = 300,
                Category = _initializer.ProviderCategoryRepository.GetAll().First(),
                Creator = _initializer.IdentityRepository.GetAll().First()
            };

            Assert.That(() => _initializer.ProviderService.Create(expected), Throws.Nothing);
            var actual = _initializer.ProviderRepository.GetBy(x => x.Title == expected.Title).Single();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_ProviderNotValid_ThrowsException()
        {
            var provider = new Provider
            {
                Price = 300,
                Title = "asd"
            };

            Assert.That(() => _initializer.ProviderService.Create(provider), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedProvider_Success()
        {
            var providerToDelete = _initializer.ProviderRepository.GetAll().First();
            _initializer.ProviderService.Delete(providerToDelete);
            foreach(var actual in _initializer.ProviderRepository.GetAll())
                Assert.AreNotEqual(providerToDelete, actual);
        }

        [Test]
        public void Delete_ProviderNotFound_ThrowsException()
        {
            var providerToDelete = new Provider();
            Assert.That(() => _initializer.ProviderService.Delete(providerToDelete), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedProvider_Success()
        {
            var expected = _initializer.ProviderRepository.GetAll().First();
            expected.Title = "New Title";
            expected.Body = "New body";
            _initializer.ProviderService.Update(expected);
            var actual = _initializer.ProviderRepository.GetBy(x => x.Id == expected.Id).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_ProviderNoValid_ThrowsException()
        {
            var provider = _initializer.ProviderRepository.GetAll().First();
            provider.Title = null;
            provider.Body = null;
            Assert.That(() => _initializer.ProviderService.Update(provider), Throws.Exception);
        }

        [Test]
        public void SearchProvider_FoundByTitleOrDescriptionProvider()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Title).Last();
            var actual = _initializer.ProviderService.SearchProvider(expected.Title).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_FoundProvider_FirstProviderById()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).First();
            var actual = _initializer.ProviderService.Get(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_FoundProvider_LastProviderById()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).Last();
            var actual = _initializer.ProviderService.Get(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllProviders_SortedListOfProviders()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.CreatedAt);
            var actual = _initializer.ProviderService.GetAll();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Providers_AllProviders_ListOfApprovedProviders()
        {
            var provider = _initializer.ProviderRepository.GetAll().First();
            provider.Approved = false;
            _initializer.ProviderRepository.Update(provider);
            _initializer.CommitProvider.CommitChanges();
            var expected = _initializer.ProviderRepository.GetBy(x => x.Approved).ToList();
            var actual = _initializer.ProviderService.Providers.ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllProviderResponses_ProviderResponses_ListOfProviderResponses()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().ToList();
            var actual = _initializer.ProviderService.GetAllProviderResponses().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchProvider_FoundProvider_ApprovedByTitleProvider()
        {
            var expected = _initializer.ProviderRepository.GetBy(x => x.Approved).ToList().Last();
            var actual = _initializer.ProviderService.SearchProvider(expected.Title).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchProvider_ProviderNotApproved_Null()
        {
            var provider = _initializer.ProviderRepository.GetAll().Last();
            provider.Approved = false;
            _initializer.ProviderRepository.Update(provider);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.ProviderService.SearchProvider(provider.Title).FirstOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void SearchProvider_FoundProvider_ProviderByBody()
        {
            var expected = _initializer.ProviderRepository.GetAll().Last();
            var actual = _initializer.ProviderService.SearchProvider(expected.Body).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ApproveProvider_ApprovedProvider_Success()
        {
            var provider = _initializer.ProviderRepository.GetAll().First();
            provider.Approved = false;
            _initializer.ProviderRepository.Update(provider);
            _initializer.CommitProvider.CommitChanges();
            _initializer.ProviderService.ApproveProvider(provider.Id);
            var actual = _initializer.ProviderRepository.GetBy(x => x.Id == provider.Id).First();
            Assert.IsTrue(actual.Approved);
        }

        [Test]
        public void ApproveProvider_ProviderNotFound_Success()
        {
            var list = _initializer.ProviderRepository.GetAll().ToList()
                .Select(x =>
                {
                    x.Approved = false;
                    return x;
                });
            foreach (var order in list)
            {
                _initializer.ProviderRepository.Update(order);
                _initializer.CommitProvider.CommitChanges();
            }
            _initializer.ProviderService.ApproveProvider(_initializer.ProviderService.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            foreach (var actual in _initializer.ProviderRepository.GetAll())
                Assert.IsFalse(actual.Approved);
        }
    }
}
