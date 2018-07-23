using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using System;

namespace ServicePlace.Website.Controllers
{
    public class ExecutorsController : Controller
    {
        ExecutorRepository repository = ExecutorSeeder.GetRepository(10);

        [HttpGet("Executors/")]
        public IActionResult Index()
        {
            return View(repository);
        }

        [HttpGet("Executors/{id}")]
        public IActionResult Get(string id)
        {
            var executor = repository.GetExecutors(Convert.ToInt32(id));
            if (executor == null) return NotFound(new
            {
                Error = $"Executor #{id} has been not found"
            });

            return View(executor);
        }
    }
}