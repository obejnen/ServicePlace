using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using ServicePlace.Models;

namespace ServicePlace.Website.Controllers
{
    public class OrdersController : BaseController
    {
        private OrderRepository repository = OrderSeeder.GetRepository(10);

        [HttpGet]
        public IActionResult Index()
        {
            return View(repository);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var order = repository.GetOrder(id);

            if (order == null) return NotFound(new {
                Error = $"Order #{id} has not been found"
            });

            return View(order);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([Bind] Order order)
        {
            if (order == null) return new StatusCodeResult(500);

            repository.AddOrder(order);
            return View("Get", repository.Orders.Last());
        }
    }
}