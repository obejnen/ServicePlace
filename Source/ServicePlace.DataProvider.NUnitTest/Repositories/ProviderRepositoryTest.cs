using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class ProviderRepositoryTest
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
        public void Take_ProvidersFrom3To5_ListOfProviders()
        {
            var providers = _initializer.ProviderRepository.GetAll().OrderBy(x => x.CreatedAt).ToList();
            var expected = new List<Provider>
            {
                providers[2],
                providers[3],
                providers[4]
            };

            var actual = _initializer.ProviderRepository.Take(2, 3).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_CreatedProvider_Success()
        {
            var expected = new Provider
            {
                Title = "Test Provider",
                Body = "Test Provider Body",
                CreatedAt = DateTime.Now,
                Approved = false,
                Price = 300,
                Category = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault(),
                Creator = _initializer.IdentityRepository.GetAll().FirstOrDefault()
            };

            _initializer.ProviderRepository.Create(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_ProviderNotValid_Exception()
        {
            var expected = new Provider
            {
                Approved = false
            };

            _initializer.ProviderRepository.Create(expected);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedProvider_Success()
        {
            var providerToDelete = _initializer.ProviderRepository.GetAll().FirstOrDefault();
            _initializer.ProviderRepository.Delete(providerToDelete);
            _initializer.CommitProvider.CommitChanges();
            foreach(var actual in _initializer.ProviderRepository.GetAll())
                Assert.AreNotEqual(providerToDelete, actual);
        }

        [Test]
        public void Delete_ProviderNotFound_Exception()
        {
            var provider = new Provider();
            Assert.That(() => _initializer.ProviderRepository.Delete(provider), Throws.InvalidOperationException);
        }

        [Test]
        public void Update_UpdatedProvider_Success()
        {
            var expected = _initializer.ProviderRepository.GetAll().FirstOrDefault();

            expected.Title = "new title";
            expected.Body = "new body";
            expected.Approved = true;

            _initializer.ProviderRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();

            var actual = _initializer.ProviderRepository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_ProviderNotValid_Success()
        {
            var expected = _initializer.ProviderRepository.GetAll().FirstOrDefault();

            expected.Title = null;
            expected.Body = null;

            _initializer.ProviderRepository.Update(expected);
            
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void GetBy_Provider_FirstProviderByTitle()
        {
            var expected = _initializer.ProviderRepository.GetAll().FirstOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Provider_LastProviderByTitle()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Provider_FirstProviderById()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).FirstOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Provider_LastProviderById()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Provider_FirstProviderByBody()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).FirstOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Body == expected.Body).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Provider_LastProviderByBody()
        {
            var expected = _initializer.ProviderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.ProviderRepository.GetBy(x => x.Body == expected.Body).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllProviders_ListOfAllProviders()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();

            var expected = new List<Provider>
            {
                _initializer.ProviderRepository.GetBy(x => x.Title == "Title1").First(),
                _initializer.ProviderRepository.GetBy(x => x.Title == "Title2").First(),
                _initializer.ProviderRepository.GetBy(x => x.Title == "Title3").First()
            };

            var actual = _initializer.ProviderRepository.GetAll().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
