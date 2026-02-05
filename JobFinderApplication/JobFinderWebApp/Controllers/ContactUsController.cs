using Microsoft.AspNetCore.Mvc;

namespace JobFinderWebApp.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
