using Microsoft.AspNetCore.Mvc;

namespace Dividni.Controllers
{
    public class TutorialController : Controller
    {
        public IActionResult Simple()
        {
            return View();
        }

        public IActionResult Advanced()
        {
            return View();
        }

        public IActionResult Assessment()
        {
            return View();
        }
    }
}