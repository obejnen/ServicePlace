using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;

namespace ServicePlace.Website.Controllers
{
    public class ExecutorsController : Controller
    {
        ExecutorRepository repository = ExecutorSeeder.GetRepository(10);
        public IActionResult Index()
        {
            return View(repository);
        }
    }
}