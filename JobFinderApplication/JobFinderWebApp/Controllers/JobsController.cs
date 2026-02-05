using Microsoft.AspNetCore.Mvc;

namespace JobFinderWebApp.Controllers
{
    public class JobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
