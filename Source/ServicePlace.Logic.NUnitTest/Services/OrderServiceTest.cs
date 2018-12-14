using System.Collections.Generic;
using NUnit.Framework;
using ServicePlace.DataInitializer;
using System.Linq;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.NUnitTest.Services
{
    [TestFixture]
    public class OrderServiceTest
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
        public void Create_CreatedOrder_ThrowsNoException()
        {
            var order = new Order
            {
                Title = "Order title",
                Body = "Order body",
                Category = _initializer.OrderCategoryRepository.GetAll().First(),
                Creator = _initializer.IdentityRepository.GetAll().First(),
            };

            Assert.That(() => _initializer.OrderService.Create(order), Throws.Nothing);
        }

        [Test]
        public void Create_OrderNotValid_ThrowsException()
        {
            var order = new Order
            {
                Title = null,
                Category = new OrderCategory()
            };

            Assert.That(() => _initializer.OrderService.Create(order), Throws.Exception);
        }

        [Test]
        public void Delete_DeletedOrder_Success()
        {
            var orderToDelete = _initializer.OrderRepository.GetAll().First();
            _initializer.OrderService.Delete(orderToDelete);
            foreach (var actual in _initializer.OrderRepository.GetAll())
                Assert.AreNotEqual(orderToDelete, actual);
        }

        [Test]
        public void Delete_OrderNotFound_ThrowsException()
        {
            var orderToDelete = new Order();
            Assert.That(() => _initializer.OrderService.Delete(orderToDelete), Throws.Exception);
        }

        [Test]
        public void Update_UpdatedOrder_TurnApprovedFalse()
        {
            var expected = _initializer.OrderRepository.GetAll().First();
            expected.Title = "new title";
            _initializer.OrderService.Update(expected);
            var actual = _initializer.OrderRepository.GetBy(x => x.Id == expected.Id).First();
            Assert.IsFalse(actual.Approved);
        }

        [Test]
        public void Update_UpdatedOrder_Success()
        {
            var expected = _initializer.OrderRepository.GetAll().First();
            expected.Title = "new order title";
            expected.Body = "new order body";
            _initializer.OrderService.Update(expected);
            expected.Approved = false;
            var actual = _initializer.OrderRepository.GetBy(x => x.Id == expected.Id).SingleOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_OrderNotValid_ThrowsException()
        {
            var expected = _initializer.OrderRepository.GetAll().First();
            expected.Title = null;
            Assert.That(() => _initializer.OrderService.Update(expected), Throws.Exception);
        }

        [Test]
        public void CloseOrder_ClosedOrder_Success()
        {
            var order = _initializer.OrderRepository.GetAll().First();
            _initializer.OrderService.CloseOrder(order.Id);
            var actual = _initializer.OrderRepository.GetBy(x => x.Id == order.Id).First();
            Assert.IsTrue(actual.Closed);
        }

        [Test]
        public void CloseOrder_OrderNotFound_Success()
        {
            _initializer.OrderService.CloseOrder(_initializer.OrderRepository.GetAll().OrderByDescending(x => x.Id).First().Id + 1);
            foreach (var actual in _initializer.OrderRepository.GetAll())
                Assert.IsFalse(actual.Closed);
        }

        [Test]
        public void CompleteOrder_CompletedOrder_Success()
        {
            var orderResponse = _initializer.OrderResponseRepository.GetAll().First();
            var order = orderResponse.Order;
            _initializer.OrderService.CompleteOrder(order.Id, orderResponse.Id);
            var expectedOrder = _initializer.OrderRepository.GetBy(x => x.Id == order.Id).Single();
            var expectedOrderResponse =
                _initializer.OrderResponseRepository.GetBy(x => x.Id == orderResponse.Id).Single();

            Assert.IsTrue(expectedOrder.Closed);
            Assert.IsTrue(expectedOrderResponse.Completed);
        }

        [Test]
        public void CompleteOrder_WrongOrderId_OrderNotCompleted()
        {
            var orderResponse = _initializer.OrderResponseRepository.GetAll().First();
            var order = _initializer.OrderRepository.GetBy(x => x.Id != orderResponse.Order.Id).First();
            _initializer.OrderService.CompleteOrder(order.Id, orderResponse.Id);
            var expectedOrder = _initializer.OrderRepository.GetBy(x => x.Id == order.Id).Single();
            var expectedOrderResponse =
                _initializer.OrderResponseRepository.GetBy(x => x.Id == orderResponse.Id).Single();

            Assert.IsFalse(expectedOrder.Closed);
            Assert.IsFalse(expectedOrderResponse.Completed);
        }

        [Test]
        public void ApproveOrder_ApprovedOrder_Success()
        {
            var order = _initializer.OrderRepository.GetAll().First();
            order.Approved = false;
            _initializer.OrderRepository.Update(order);
            _initializer.CommitProvider.CommitChanges();
            _initializer.OrderService.ApproveOrder(order.Id);
            var actual = _initializer.OrderRepository.GetBy(x => x.Id == order.Id).Single();
            Assert.IsTrue(actual.Approved);
        }

        [Test]
        public void ApproveOrder_OrderNotFound_Success()
        {
            var list = _initializer.OrderRepository.GetAll().ToList()
                .Select(x =>
                {
                    x.Approved = false;
                    return x;
                });
            foreach (var order in list)
            {
                _initializer.OrderRepository.Update(order);
                _initializer.CommitProvider.CommitChanges();
            }
            _initializer.OrderService.ApproveOrder(_initializer.OrderRepository.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            foreach (var actual in _initializer.OrderRepository.GetAll())
                Assert.IsFalse(actual.Approved);
        }

        [Test]
        public void Get_Order_FirstOrderById()
        {
            var expected = _initializer.OrderRepository.GetAll().First();
            var actual = _initializer.OrderService.Get(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_Order_LastOrderById()
        {
            var expected = _initializer.OrderRepository.GetAll().Last();
            var actual = _initializer.OrderService.Get(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get_WrongOrderId_Null()
        {
            var actual =
                _initializer.OrderService.Get(_initializer.OrderRepository.GetAll().OrderBy(x => x.Id).Last().Id + 1);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetAll_AllOrders_SortedListOfOrders()
        {
            var expected = _initializer.OrderRepository.GetAll().OrderBy(x => x.CreatedAt);
            var actual = _initializer.OrderService.GetAll();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Orders_AllApprovedOrders_ListOfApprovedOrders()
        {
            var order = _initializer.OrderRepository.GetAll().First();
            order.Approved = false;
            _initializer.OrderRepository.Update(order);
            _initializer.CommitProvider.CommitChanges();
            var orders = _initializer.OrderRepository.GetAll();
            var expected = orders.Reverse().Take(orders.Count() - 1).Reverse();
            var actual = _initializer.OrderService.Orders;
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllOrderResponses()
        {
            var expected = _initializer.OrderResponseRepository.GetAll().ToList();
            var actual = _initializer.OrderService.GetAllOrderResponses();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchOrder_FoundOrder_ApprovedOrderByTitle()
        {
            var expected = _initializer.OrderService.GetAll().OrderBy(x => x.Title).Last();
            var actual = _initializer.OrderService.SearchOrder(expected.Title, -1).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchOrder_OrderNotApproved_Null()
        {
            var order = _initializer.OrderService.GetAll().OrderBy(x => x.Title).Last();
            order.Approved = false;
            _initializer.OrderRepository.Update(order);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.OrderService.SearchOrder(order.Title, -1).FirstOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void SearchOrder_OrderClosed_Null()
        {
            var order = _initializer.OrderService.GetAll().OrderBy(x => x.Title).Last();
            order.Closed = true;
            _initializer.OrderRepository.Update(order);
            _initializer.CommitProvider.CommitChanges();
            var actual = _initializer.OrderService.SearchOrder(order.Title, -1).FirstOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void SearchOrder_FoundOrder_ApprovedOrderByTitleInCategory()
        {
            var expected = _initializer.OrderService.GetAll().OrderBy(x => x.Title).Last();
            var actual = _initializer.OrderService.SearchOrder(expected.Title, expected.Category.Id).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchOrder_FoundOrder_ApprovedOrderByBody()
        {
            var expected = _initializer.OrderService.GetAll().OrderBy(x => x.Body).Last();
            var actual = _initializer.OrderService.SearchOrder(expected.Body, -1).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Take_OrdersInRange_ListOfApprovedOrdersInRange()
        {
            var orders = _initializer.OrderRepository.GetAll().ToList();
            const int skip = 3;
            const int take = 2;
            var expected = orders.Skip(skip).Take(take);
            var actual = _initializer.OrderService.Take(skip, take);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateResponse_CreatedResponse_ThrowsNoException()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.Providers.Count > 0).First();
            var order = _initializer.OrderRepository.GetBy(x => x.Creator.Id != user.Id).First();
            var expected = new OrderResponse
            {
                Comment = "response comment",
                Price = 300,
                Order = order,
                Creator = user,
                Provider = user.Providers.First()
            };
            Assert.That(() => _initializer.OrderService.CreateResponse(expected), Throws.Nothing);
            var actual = _initializer.OrderResponseRepository.GetBy(x => x.Comment == expected.Comment).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateResponse_ResponseNotValid_ThrowsException()
        {
            var response = new OrderResponse
            {
                Price = 300
            };

            Assert.That(() => _initializer.OrderService.CreateResponse(response), Throws.Exception);
        }

        [Test]
        public void DeleteResponse_DeletedResponse_Success()
        {
            var responseToDelete = _initializer.OrderResponseRepository.GetAll().First();
            _initializer.OrderService.DeleteResponse(responseToDelete);
            foreach (var actual in _initializer.OrderResponseRepository.GetAll())
                Assert.AreNotEqual(responseToDelete, actual);
        }

        [Test]
        public void DeleteResponse_ResponseNotFound_ThrowsException()
        {
            var responseToDelete = new OrderResponse();
            Assert.That(() => _initializer.OrderService.DeleteResponse(responseToDelete), Throws.Exception);
        }

        [Test]
        public void GetOrderResponses_OrderResponses_ListOfOrderResponsesByOrderId()
        {
            _initializer = new Initializer(100);
            _initializer.InitializeDb();
            var orderResponses = _initializer.OrderResponseRepository.GetAll().ToList();
            var orderId = orderResponses
                .GroupBy(order => order.Order.Id)
                .OrderByDescending(order => order.Count()).First().Key;
            var expected = _initializer
                .OrderResponseRepository
                .GetBy(x => x.Id == orderId).ToList();
            var actual = _initializer.OrderService.GetOrderResponses(expected.First().Order.Id).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUserOrders_Orders_ListOfOrdersByUserId()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.Orders.Count > 0).First();
            var expected = user.Orders;
            var actual = _initializer.OrderService.GetUserOrders(user.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUserResponses_OrderResponses_ListOfOrderResponsesByUserId()
        {
            var user = _initializer.IdentityRepository.GetBy(x => x.OrderResponses.Count > 0).First();
            var expected = user.OrderResponses;
            var actual = _initializer.OrderService.GetUserResponses(user.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCategories_OrderCategories_ListOfAllOrderCategories()
        {
            var expected = _initializer.OrderCategoryRepository.GetAll().ToList();
            var actual = _initializer.OrderService.GetCategories().ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCategory_Category_CategoryById()
        {
            var expected = _initializer.OrderCategoryRepository.GetAll().First();
            var actual = _initializer.OrderService.GetCategory(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCategory_WrongCategoryId_Null()
        {
            var actual =
                _initializer.OrderService.GetCategory(_initializer.OrderCategoryRepository.GetAll().OrderBy(x => x.Id)
                                                          .Last().Id + 1);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetByCategory_Orders_OrdersByCategoryId()
        {
            var category = _initializer.OrderCategoryRepository.GetAll().First();
            var expected = _initializer.OrderRepository.GetBy(x => x.Category.Id == category.Id).ToList();
            var actual = _initializer.OrderService.GetByCategory(category.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetByCategory_WrongCategoryId_EmptyList()
        {
            var actual =
                _initializer.OrderService.GetByCategory(_initializer.OrderCategoryRepository.GetAll().OrderBy(x => x.Id)
                    .Last().Id + 1).ToList();
            CollectionAssert.AreEqual(new List<Order>(), actual);
        }

        [Test]
        public void CreateCategory_CreatedCategory_ThrowsNoExceptions()
        {
            var expected = new OrderCategory
            {
                Name = "new category"
            };

            Assert.That(() => _initializer.OrderService.CreateCategory(expected), Throws.Nothing);
            var actual = _initializer.OrderCategoryRepository.GetBy(x => x.Name == expected.Name).First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateCategory_CategoryNotValid_ThrowsException()
        {
            var category = new OrderCategory();

            Assert.That(() => _initializer.OrderService.CreateCategory(category), Throws.Exception);
        }
    }
}
