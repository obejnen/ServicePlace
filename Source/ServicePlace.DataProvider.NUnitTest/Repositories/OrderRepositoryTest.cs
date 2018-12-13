using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using NUnit.Framework;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class OrderRepositoryTest
    {
        private readonly Initializer _initializer;

        public OrderRepositoryTest()
        {
            _initializer = new Initializer(10);
        }

        [SetUp]
        public void Init()
        {
            _initializer.ClearDb();
            _initializer.InitializeDb();
        }

        [TearDown]
        public void Dispose()
        {
            _initializer.ClearDb();
        }

        [Test]
        public void CloseOrder_ClosedOrder()
        {
            var repository = _initializer.OrderRepository;
            var expected = repository.GetAll().First();
            expected.Closed = true;
            repository.CloseOrder(expected.Id);
            var actual = repository.GetBy(x => x.Id == expected.Id).First();
            Assert.AreEqual(expected.Closed, actual.Closed);
        }

        [Test]
        public void Take_OrdersFrom3To5_Orders()
        {
            var repository = _initializer.OrderRepository;
            var orders = repository.GetAll().OrderBy(x => x.CreatedAt).ToList();
            var expected = new List<Order>
            {
                orders[2],
                orders[3],
                orders[4]
            };
            var actual = repository.Take(2, 3).ToList();
            for(var i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i].Id, actual[i].Id);
        }

        [Test]
        public void Create_CreatedOrder_Success()
        {
            var repository = _initializer.OrderRepository;
            var categoryRepository = _initializer.OrderCategoryRepository;
            var identityRepository = _initializer.IdentityRepository;
            var commitProvider = _initializer.CommitProvider;

            var expected = new Order
            {
                Approved = false,
                Body = "order body",
                Category = categoryRepository.GetAll().First(),
                Closed = false,
                CreatedAt = DateTime.Now,
                Creator = identityRepository.GetAll().First(),
                Title = "order title"
            };
            repository.Create(expected);
            commitProvider.CommitChanges();
            var actual = repository.GetAll().FirstOrDefault(x => x.Title == "order title");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void Create_Exception_Failed()
        {
            var repository = _initializer.OrderRepository;
            var categoryRepository = _initializer.OrderCategoryRepository;
            var identityRepository = _initializer.IdentityRepository;
            var commitProvider = _initializer.CommitProvider;

            var expected = new Order
            {
                Approved = true,
                Title = "title",
                CreatedAt = DateTime.Now,
                Creator = identityRepository.GetAll().First(),
                Category = categoryRepository.GetAll().First()
            };
            repository.Create(expected);
            Assert.That(() => commitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedOrder_Success()
        {
            var repository = _initializer.OrderRepository;
            var commitProvider = _initializer.CommitProvider;
            var providerResponseRepository = _initializer.ProviderResponseRepository;
            var orderToDelete = repository.GetAll().First();
            providerResponseRepository
                .GetAll()
                .Where(x => x.Order.Id == orderToDelete.Id)
                .Select(x =>
                {
                    providerResponseRepository.Delete(x);
                    return new ProviderResponse();
                });
            repository.Delete(orderToDelete);
            commitProvider.CommitChanges();
            var actual = repository.GetAll().FirstOrDefault(x => x.Id == orderToDelete.Id);
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UpdatedOrder_Success()
        {
            var repository = _initializer.OrderRepository;
            var commitProvider = _initializer.CommitProvider;

            var actual = repository.GetAll().First();
            actual.Title = "new title";
            actual.Body = "new body";
            actual.Closed = true;
            actual.Approved = true;
            repository.Update(actual);
            commitProvider.CommitChanges();

            var expected = repository.GetBy(x => x.Id == actual.Id).First();

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void GetBy_Order_OrderByTitle()
        {
            var repository = _initializer.OrderRepository;
            var actual = repository.GetAll().First();
            var expected = repository.GetBy(x => x.Title == actual.Title);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void GetBy_Order_OrderById()
        {
            var repository = _initializer.OrderRepository;
            var actual = repository.GetAll().First();
            var expected = repository.GetBy(x => x.Id == actual.Id);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void GetBy_Order_OrderByBody()
        {
            var repository = _initializer.OrderRepository;
            var actual = repository.GetAll().First();
            var expected = repository.GetBy(x => x.Body == actual.Body);
            Assert.AreEqual(actual, expected);
        }
    }
}
