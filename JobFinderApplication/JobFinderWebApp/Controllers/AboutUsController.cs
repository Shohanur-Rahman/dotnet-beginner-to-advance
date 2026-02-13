using Microsoft.AspNetCore.Mvc;

namespace JobFinderWebApp.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
