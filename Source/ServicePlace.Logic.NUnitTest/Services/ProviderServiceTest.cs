using System;
using System.Collections.Generic;
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
            foreach (var actual in _initializer.ProviderRepository.GetAll())
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

            _initializer.ProviderService.ApproveProvider(
                _initializer.ProviderService.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            foreach (var actual in _initializer.ProviderRepository.GetAll())
                Assert.IsFalse(actual.Approved);
        }

        [Test]
        public void Take_ProvidersInRange_ListOfApprovedProvidersInRange()
        {
            var providers = _initializer.ProviderRepository.GetAll().ToList();
            const int skip = 3;
            const int take = 2;
            var expected = providers.Skip(skip).Take(take);
            var actual = _initializer.ProviderService.Take(skip, take);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUserProviders_Providers_ListOfProvidersByUserId()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.Providers.Count > 0).First();
            var expected = _initializer.ProviderRepository.GetBy(x => x.Creator.Id == user.Id).ToList();
            var actual = _initializer.ProviderService.GetUserProviders(user.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPage_Providers_ListOfProvidersOnFullPage()
        {
            const int page = 2;
            const int perPage = 3;
            var expected = _initializer.ProviderRepository.GetAll().Skip(perPage).Take(perPage).ToList();
            var actual = _initializer.ProviderService.GetPage(page, perPage);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPage_Providers_ListOfProvidersOnNonFullPage()
        {
            _initializer = new Initializer(11);
            _initializer.InitializeDb();
            const int page = 6;
            const int perPage = 2;
            var expected = _initializer.ProviderRepository.GetAll().Skip(10).Take(1).ToList();
            var actual = _initializer.ProviderService.GetPage(page, perPage);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPage_PartOfProviders_ListOfProvidersOnFullPage()
        {
            var providersList = _initializer.ProviderRepository.GetAll().Skip(2).ToList();
            const int page = 2;
            const int perPage = 2;
            var expected = providersList.Skip(2).Take(2);
            var actual = _initializer.ProviderService.GetPage(providersList, page, perPage);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPage_PartOfProviders_ListOfProvidersOnNonFullPage()
        {
            _initializer = new Initializer(11);
            _initializer.InitializeDb();
            var providersList = _initializer.ProviderRepository.GetAll().Skip(2).ToList();
            const int page = 5;
            const int perPage = 2;
            var expected = providersList.Skip(8).Take(1).ToList();
            var actual = _initializer.ProviderService.GetPage(providersList, page, perPage);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateResponse_CreatedResponse_ThrowsNoException()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.Providers.Count > 0).First();
            var order = _initializer.OrderRepository.GetBy(x => x.Creator.Id != user.Id).First();
            var orderResponse = new OrderResponse
            {
                Comment = "Comment to find",
                Creator = user,
                Order = order,
                Provider = user.Providers.First(),
                Price = 300,
                CreatedAt = DateTime.Now
            };
            _initializer.OrderResponseRepository.Create(orderResponse);
            _initializer.CommitProvider.CommitChanges();

            var expectedOrder = _initializer.OrderRepository.GetBy(x => x.Id == order.Id).Single();

            var expected = new ProviderResponse
            {
                Comment = "Comment",
                Provider = _initializer.IdentityRepository.GetBy(x => x.Id == user.Id).Single().Providers.First(),
                Order = expectedOrder,
                Creator = expectedOrder.Creator
            };

            Assert.That(() => _initializer.ProviderService.CreateResponse(expected), Throws.Nothing);
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Comment == expected.Comment).Single();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateResponse_ResponseNotValid_ThrowsException()
        {
            var response = new ProviderResponse
            {
                Comment = null,
                Creator = new User(),
                Provider = new Provider(),
                Order = new Order()
            };

            Assert.That(() => _initializer.ProviderService.CreateResponse(response), Throws.Exception);
        }

        [Test]
        public void DeleteResponse_DeletedResponse_Success()
        {
            var responseToDelete = _initializer.ProviderResponseRepository.GetAll().First();
            _initializer.ProviderService.DeleteResponse(responseToDelete);
            foreach (var actual in _initializer.ProviderResponseRepository.GetAll())
                Assert.AreNotEqual(responseToDelete, actual);
        }

        [Test]
        public void DeleteResponse_ResponseNotFound_ThrowsException()
        {
            var responseToDelete = new ProviderResponse();
            Assert.That(() => _initializer.ProviderService.DeleteResponse(responseToDelete), Throws.Exception);
        }

        [Test]
        public void GetProviderResponses_ProviderResponses_ListOfProviderResponsesByProviderId()
        {
            var providerResponse = _initializer.ProviderResponseRepository.GetAll().First();
            var expected =
                _initializer.ProviderResponseRepository.GetBy(x => x.Provider.Id == providerResponse.Provider.Id)
                    .ToList();
            var actual = _initializer.ProviderService.GetProviderResponses(providerResponse.Provider.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCategory_Category_CategoryById()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().First();
            var actual = _initializer.ProviderService.GetCategory(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUserResponses_ProviderResponses_ListOfProviderResponsesByUserId()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.ProviderResponses.Count > 0).First();
            var expected = _initializer.ProviderResponseRepository.GetBy(x => x.Creator.Id == user.Id).ToList();
            var actual = _initializer.ProviderService.GetUserResponses(user.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetByCategory_Providers_ProvidersByCategoryId()
        {
            var category = _initializer.ProviderCategoryRepository.GetAll().First();
            var expected = _initializer.ProviderRepository.GetBy(x => x.Category.Id == category.Id).ToList();
            var actual = _initializer.ProviderService.GetByCategory(category.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetByCategory_WrongCategoryId_EmptyList()
        {
            var actual =
                _initializer.ProviderService.GetByCategory(
                    _initializer.ProviderCategoryRepository.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            CollectionAssert.AreEqual(new List<Provider>(), actual);
        }

        [Test]
        public void GetCategories_ProviderCategories_ListOfAllProviderCategories()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().ToList();
            var actual = _initializer.ProviderService.GetCategories();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateCategory_CreatedCategory_ThrowsNoException()
        {
            var expected = new ProviderCategory
            {
                Name = "New category"
            };

            Assert.That(() => _initializer.ProviderService.CreateCategory(expected), Throws.Nothing);
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Name == expected.Name).Single();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateCategory_CategoryNotValid_ThrowsException()
        {
            Assert.That(() => _initializer.ProviderService.CreateCategory(new ProviderCategory()), Throws.Exception);
        }

        [Test]
        public void DeleteCategory_DeletedCategory_Success()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().First();
            _initializer.ProviderService.DeleteCategory(expected);
            foreach (var actual in _initializer.ProviderCategoryRepository.GetAll())
                Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void DeleteCategory_CategoryNotFound_ThrowsException()
        {
            var providerCategory = new ProviderCategory();
            Assert.That(() => _initializer.ProviderService.DeleteCategory(providerCategory), Throws.Exception);
        }

        [Test]
        public void UpdateCategory_UpdatedCategory_Success()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().ToList().First();
            expected.Name = "new category name";
            Assert.That(() => _initializer.ProviderService.UpdateCategory(expected), Throws.Nothing);
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Id == expected.Id).Single();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateCategory_CategoryNotValid_ThrowsException()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().First();
            expected.Name = null;
            Assert.That(() => _initializer.ProviderService.UpdateCategory(expected), Throws.Exception);
        }
    }
}
