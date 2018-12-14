using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class ProviderCategoryRepositoryTest
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
        public void Create_CreatedCategory_ThrowsNoExceptions()
        {
            var category = new ProviderCategory
            {
                Name = "New category"
            };

            _initializer.ProviderCategoryRepository.Create(category);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Nothing);
        }

        [Test]
        public void Create_CategoryNotValid_ThrowsException()
        {
            var category = new ProviderCategory
            {
                Name = null
            };

            _initializer.ProviderCategoryRepository.Create(category);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedCategory_Success()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault();
            expected.Name = "New category name";
            _initializer.ProviderCategoryRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_CategoryNotValid_ThrowsException()
        {
            var category = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault();
            category.Name = null;
            _initializer.ProviderCategoryRepository.Update(category);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedCategory_Success()
        {
            var category = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault();
            _initializer.ProviderCategoryRepository.Delete(category);
            _initializer.CommitProvider.CommitChanges();
            foreach (var actual in _initializer.ProviderCategoryRepository.GetAll())
                Assert.AreNotEqual(category, actual);
        }

        [Test]
        public void Delete_CategoryNotFound_ThrowsException()
        {
            var category = new ProviderCategory();
            Assert.That(() => _initializer.ProviderCategoryRepository.Delete(category), Throws.Exception);
        }

        [Test]
        public void GetBy_FoundCategory_FirstCategoryByName()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault();
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Name == expected.Name).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundCategory_LastCategoryByName()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().LastOrDefault();
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Name == expected.Name).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundCategory_FirstCategoryById()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().FirstOrDefault();
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundCategory_LastCategoryById()
        {
            var expected = _initializer.ProviderCategoryRepository.GetAll().LastOrDefault();
            var actual = _initializer.ProviderCategoryRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllCategories_ListOfAllCategories()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();

            var expected = new List<ProviderCategory>
            {
                _initializer.ProviderCategoryRepository.GetBy(x => x.Name == "Name1").FirstOrDefault(),
                _initializer.ProviderCategoryRepository.GetBy(x => x.Name == "Name2").FirstOrDefault(),
                _initializer.ProviderCategoryRepository.GetBy(x => x.Name == "Name3").FirstOrDefault(),
            };

            var actual = _initializer.ProviderCategoryRepository.GetAll().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
