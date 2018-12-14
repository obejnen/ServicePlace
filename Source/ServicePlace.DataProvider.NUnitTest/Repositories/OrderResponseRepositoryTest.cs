using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class OrderResponseRepositoryTest
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
            var user = _initializer.IdentityRepository.GetBy(x => x.Providers.Count > 0).FirstOrDefault();
            var order = _initializer.OrderRepository.GetBy(x => x.Creator.Id != user.Id).FirstOrDefault();
            var response = new OrderResponse
            {
                Comment = "Comment",
                Completed = false,
                Creator = user,
                Order = order,
                Provider = user.Providers.FirstOrDefault(),
                Price = 300,
                CreatedAt = DateTime.Now,
            };

            _initializer.OrderResponseRepository.Create(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Nothing);
        }

        [Test]
        public void Create_ResponseNotValid_ThrowsException()
        {
            var response = new OrderResponse
            {
                Comment = null
            };

            _initializer.OrderResponseRepository.Create(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedResponse_Success()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().FirstOrDefault();
            expected.Comment = "New response name";
            _initializer.OrderResponseRepository.Update(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_responseNotValid_ThrowsException()
        {
            var response = _initializer.OrderResponseRepository.GetAll().FirstOrDefault();
            response.Order = null;
            _initializer.OrderResponseRepository.Update(response);
            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedResponse_Success()
        {
            var response = _initializer.OrderResponseRepository.GetAll().FirstOrDefault();
            _initializer.OrderResponseRepository.Delete(response);
            _initializer.CommitProvider.CommitChanges();
            foreach (var actual in _initializer.OrderResponseRepository.GetAll())
                Assert.AreNotEqual(response, actual);
        }

        [Test]
        public void Delete_responseNotFound_ThrowsException()
        {
            var response = new OrderResponse();
            Assert.That(() => _initializer.OrderResponseRepository.Delete(response), Throws.Exception);
        }

        [Test]
        public void GetBy_FoundResponse_FirstResponseByComment()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().FirstOrDefault();
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Comment == expected.Comment).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_LastResponseByComment()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().LastOrDefault();
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Comment == expected.Comment).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_FirstResponseById()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().FirstOrDefault();
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_FoundResponse_LastResponseById()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().LastOrDefault();
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllResponses_ListOfAllResponses()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();

            var expected = new List<OrderResponse>
            {
                _initializer.OrderResponseRepository.GetBy(x => x.Comment == "Comment1").FirstOrDefault(),
                _initializer.OrderResponseRepository.GetBy(x => x.Comment == "Comment2").FirstOrDefault(),
                _initializer.OrderResponseRepository.GetBy(x => x.Comment == "Comment3").FirstOrDefault(),
            };

            var actual = _initializer.OrderResponseRepository.GetAll().OrderBy(x => x.Comment).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
