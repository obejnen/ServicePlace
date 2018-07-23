using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using ServicePlace.Models;

namespace ServicePlace.Website.Controllers
{
    public class ExecutorsController : BaseController
    {
        private ExecutorRepository repository;
        public ExecutorsController()
        {
            repository = ExecutorSeeder.GetRepository(10);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(repository);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var executor = repository.GetExecutors(id);
            if (executor == null) return NotFound(new
            {
                Error = $"Executor #{id} has been not found"
            });

            return View(executor);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(Executor executor)
        {
            if (executor == null) return new StatusCodeResult(500);

            repository.AddExecutor(executor);
            return View("Get", repository.Executors.Last());
        }
    }
}