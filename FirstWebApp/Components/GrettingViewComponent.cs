using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Components
{
    public class GrettingViewComponent : ViewComponent
    {

        public GrettingViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(string name)
        {
            WelcomeMessage welcomeMessage = new WelcomeMessage
            {
                UserName = name,
                Message = $"Hello, {name}! Welcome to our website."
            };
            return View("~/Components/Views/_WelcomeMessage.cshtml", welcomeMessage);
        }
    }
}
