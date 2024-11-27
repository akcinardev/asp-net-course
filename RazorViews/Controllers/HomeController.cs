using Microsoft.AspNetCore.Mvc;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
