using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Repositories;
using System.Linq;

namespace ServicePlace.DataProvider.Test.Repositories
{
    [TestClass]
    public class OrderRepositoryTest
    {
        [TestMethod]
        public void CloseOrder_ClosedOrder_Success()
        {
            var applicationContext = new ApplicationContext();
            var orderRepository = new OrderRepository(applicationContext);
            var expected = orderRepository.GetBy(x => x.Id == 1).First();
            expected.Closed = true;
            orderRepository.CloseOrder(1);
            var actual = orderRepository.GetBy(x => x.Id == 1).First();
            Assert.AreEqual(expected.Closed, actual.Closed);
        }
    }
}
