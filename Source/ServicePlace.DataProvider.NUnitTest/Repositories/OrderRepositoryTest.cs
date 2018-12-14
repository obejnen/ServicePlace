using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.DataInitializer;

namespace ServicePlace.DataProvider.NUnitTest.Repositories
{
    [TestFixture]
    public class OrderRepositoryTest
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
        public void CloseOrder_ClosedOrder_Success()
        {
            var repository = _initializer.OrderRepository;
            var expected = repository.GetAll().FirstOrDefault();
            expected.Closed = true;
            repository.CloseOrder(expected.Id);
            var actual = repository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected.Closed, actual.Closed);
        }

        [Test]
        public void CloseOrder_OrderNotFound_Success()
        {
            _initializer.OrderRepository.CloseOrder(_initializer.OrderRepository.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            foreach (var order in _initializer.OrderRepository.GetAll()) Assert.IsFalse(order.Closed);
        }

        [Test]
        public void Take_OrdersFrom3To5_ListOfOrders()
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
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_CreatedOrder_Success()
        {
            var expected = new Order
            {
                Approved = false,
                Body = "order body",
                Category = _initializer.OrderCategoryRepository.GetAll().FirstOrDefault(),
                Closed = false,
                CreatedAt = DateTime.Now,
                Creator = _initializer.IdentityRepository.GetAll().FirstOrDefault(),
                Title = "order title"
            };
            _initializer.OrderRepository.Create(expected);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.OrderRepository.GetAll().FirstOrDefault(x => x.Title == expected.Title);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_OrderNotValid_Exception()
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
                Creator = identityRepository.GetAll().FirstOrDefault(),
                Category = categoryRepository.GetAll().FirstOrDefault()
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
            var orderToDelete = repository.GetAll().FirstOrDefault();
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
        public void Delete_OrderNotFound_Exception()
        {
            var order = new Order();
            Assert.That(() => _initializer.OrderRepository.Delete(order), Throws.InvalidOperationException);
        }

        [Test]
        public void Update_UpdatedOrder_Success()
        {
            var repository = _initializer.OrderRepository;
            var commitProvider = _initializer.CommitProvider;

            var expected = repository.GetAll().FirstOrDefault();
            expected.Title = "new title";
            expected.Body = "new body";
            expected.Closed = true;
            expected.Approved = true;
            repository.Update(expected);
            commitProvider.CommitChanges();

            var actual = repository.GetBy(x => x.Id == expected.Id).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_OrderNotValid_Exception()
        {
            var expected = _initializer.OrderRepository.GetAll().FirstOrDefault();
            expected.Title = null;
            expected.Body = null;
            _initializer.OrderRepository.Update(expected);

            Assert.That(() => _initializer.CommitProvider.CommitChanges(), Throws.Exception);
        }

        [Test]
        public void GetBy_Order_FirstOrderByTitle()
        {
            var repository = _initializer.OrderRepository;
            var expected = repository.GetAll().FirstOrDefault();
            var actual = repository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Order_LastOrderByTitle()
        {
            var expected = _initializer.OrderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.OrderRepository.GetBy(x => x.Title == expected.Title).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Order_FirstOrderById()
        {
            var repository = _initializer.OrderRepository;
            var expected = repository.GetAll().FirstOrDefault();
            var actual = repository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Order_LastOrderById()
        {
            var expected = _initializer.OrderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.OrderRepository.GetBy(x => x.Id == expected.Id).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Order_FirstOrderByBody()
        {
            var repository = _initializer.OrderRepository;
            var expected = repository.GetAll().FirstOrDefault();
            var actual = repository.GetBy(x => x.Body == expected.Body).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBy_Order_LastOrderByBody()
        {
            var expected = _initializer.OrderRepository.GetAll().OrderBy(x => x.Id).LastOrDefault();
            var actual = _initializer.OrderRepository.GetBy(x => x.Body == expected.Body).FirstOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_AllOrders_ListOfAllOrders()
        {
            _initializer = new Initializer(3);
            _initializer.InitializeDb();
            var expected = new List<Order>
            {
                _initializer.OrderRepository.GetBy(x => x.Title == "Title1").FirstOrDefault(),
                _initializer.OrderRepository.GetBy(x => x.Title == "Title2").FirstOrDefault(),
                _initializer.OrderRepository.GetBy(x => x.Title == "Title3").FirstOrDefault()
            };

            var actual = _initializer.OrderRepository.GetAll().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
