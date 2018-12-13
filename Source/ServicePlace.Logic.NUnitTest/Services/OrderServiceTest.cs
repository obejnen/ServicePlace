using NUnit.Framework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Services;
using ServicePlace.Model.ViewModels;
using ServicePlace.DataInitializer;
using System.Linq;

namespace ServicePlace.Logic.NUnitTest.Services
{
    [TestFixture]
    public class OrderServiceTest
    {
        [Test]
        public void SearchOrder_FoundByTitleOrDescriptionOrder()
        {
            var initializer = new Initializer(10);
            initializer.ClearDb();
            initializer.InitializeDb();
            var service = initializer.OrderService;
            var expected = service.GetAll().OrderBy(x => x.Title).First();
            var actual = service.SearchOrder(expected.Title, -1).OrderBy(x => x.Title).First();
            Assert.AreEqual(expected, actual);
            initializer.ClearDb();
        }
    }
}
