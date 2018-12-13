using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Services;
using ServicePlace.Model.ViewModels;
using System.Linq;
using ServicePlace.DataProvider.ContextProviders;

namespace ServicePlace.Logic.Test.Services
{
    [TestClass]
    public class OrderServiceTest
    {
        public OrderService InitializeOrderService()
        {
            var applicationContext = new ApplicationContext();
            var orderRepository = new OrderRepository(applicationContext);
            var responseRepository = new OrderResponseRepository(applicationContext);
            var commitProvider = new CommitProvider(applicationContext);
            return new OrderService(orderRepository, responseRepository, null, commitProvider);
        }

        [TestMethod]
        public void SearchOrder_FoundByTitleOrDescriptionOrder_OrderWithId1()
        {
            var orderService = InitializeOrderService();
            var expected = orderService.Get(1);
            var searchQuery = "order";
            var actual = orderService.SearchOrder(searchQuery, -1).First();
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void CloseOrder_ClosedOrder_Success()
        {
            var orderService = InitializeOrderService();
            orderService.CloseOrder(1);
            var actual = orderService.Orders.First(x => x.Id == 1);
            Assert.IsTrue(actual.Closed);
        }

        [TestMethod]
        public void CompleteOrder_CompletedOrder_Success()
        {
            var orderService = InitializeOrderService();
            orderService.CompleteOrder(3, 1);
            var actual = orderService.Orders.First(x => x.Id == 3);
            Assert.IsTrue(actual.Closed);
        }

        [TestMethod]
        public void ApproveOrder_ApprovedOrder_Success()
        {
            var orderService = InitializeOrderService();
            orderService.ApproveOrder(3);
            var actual = orderService.Orders.First(x => x.Id == 3);
            Assert.IsTrue(actual.Approved);
        }
    }
}
