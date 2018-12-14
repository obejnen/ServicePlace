using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class ProviderResponseRepositoryTest
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
        public void Create_CreatedResponse_ThrowsNoExceptions()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.Orders.Count > 0).FirstOrDefault();
            var provider = _initializer.ProviderRepository.GetBy(x => x.Creator.Id != user.Id).FirstOrDefault();
            var response = new ProviderResponse
            {
                Comment = "Comment",
                Creator = user,
                Provider = provider,
                Order = user.Orders.FirstOrDefault(),
                CreatedAt = DateTime.Now,
            };

            _initializer.ProviderResponseRepository.Create(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Nothing);
        }

        [Test]
        public void Create_ResponseNotValid_ThrowsException()
        {
            var response = new ProviderResponse
            {
                Comment = null
            };

            _initializer.ProviderResponseRepository.Create(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedResponse_Success()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().FirstOrDefault();
            expected.Comment = "New response name";
            _initializer.ProviderResponseRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_responseNotValid_ThrowsException()
        {
            var response = _initializer.ProviderResponseRepository.GetAll().FirstOrDefault();
            response.Provider = null;
            _initializer.ProviderResponseRepository.Update(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedResponse_Success()
        {
            var response = _initializer.ProviderResponseRepository.GetAll().FirstOrDefault();
            _initializer.ProviderResponseRepository.Delete(response);
            _initializer.CommitProvider.CommitChanges();
            foreach (var actual in _initializer.ProviderResponseRepository.GetAll())
                Assert.AreNotEqual(response, actual);
        }

        [Test]
        public void Delete_responseNotFound_ThrowsException()
        {
            var response = new ProviderResponse();
            Assert.That(() => _initializer.ProviderResponseRepository.Delete(response), Throws.Exception);
        }

        [Test]
        public void GetBy_FoundResponse_FirstResponseByComment()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().FirstOrDefault();
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Comment == expected.Comment).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_LastResponseByComment()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().LastOrDefault();
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Comment == expected.Comment).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_FirstResponseById()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().FirstOrDefault();
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_LastResponseById()
        {
            var expected = _initializer.ProviderResponseRepository.GetAll().LastOrDefault();
            var actual = _initializer.ProviderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllResponses_ListOfAllResponses()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();

            var expected = new List<ProviderResponse>
            {
                _initializer.ProviderResponseRepository.GetBy(x => x.Comment == "Comment1").FirstOrDefault(),
                _initializer.ProviderResponseRepository.GetBy(x => x.Comment == "Comment2").FirstOrDefault(),
                _initializer.ProviderResponseRepository.GetBy(x => x.Comment == "Comment3").FirstOrDefault(),
            };

            var actual = _initializer.ProviderResponseRepository.GetAll().OrderBy(x => x.Comment).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
