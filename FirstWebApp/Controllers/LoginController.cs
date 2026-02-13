using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
