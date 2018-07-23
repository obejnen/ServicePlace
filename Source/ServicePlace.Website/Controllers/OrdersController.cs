using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using System;

namespace ServicePlace.Website.Controllers
{
    public class OrdersController : Controller
    {
        private OrderRepository repository = OrderSeeder.GetRepository(10);

        [HttpGet("Orders/")]
        public IActionResult Index()
        {
            return View(repository);
        }

        [HttpGet("Orders/{id}")]
        public IActionResult Get(string id)
        {
            var order = repository.GetOrder(Convert.ToInt32(id));

            if (order == null) return NotFound(new {
                Error = $"Order #{id} has not been found"
            });

            return View(order);
        }
    }
}