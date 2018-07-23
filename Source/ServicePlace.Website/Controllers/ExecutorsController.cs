using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using ServicePlace.ViewModels;

namespace ServicePlace.Website.Controllers
{
    public class ExecutorsController : BaseController
    {
        private ExecutorService service;
        public ExecutorsController()
        {
            service = ExecutorInitializer.GetService(10);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(service);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var executor = service.GetExecutors(id);
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

            service.AddExecutor(executor);
            return View("Get", service.Executors.Last());
        }
    }
}