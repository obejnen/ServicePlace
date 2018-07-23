using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using ServicePlace.ViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrdersController : BaseController
    {
        private OrderService service = OrderInitializer.GetService(10);

        [HttpGet]
        public IActionResult Index()
        {
            return View(service);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var order = service.GetOrder(id);

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

            service.AddOrder(order);
            return View("Get", service.Orders.Last());
        }
    }
}