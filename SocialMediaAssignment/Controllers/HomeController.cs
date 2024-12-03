using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaAssignment.Options;

namespace SocialMediaAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptions<SocialMediaLinkOptions> _options;

        public HomeController(IWebHostEnvironment webHostEnvironment, IOptions<SocialMediaLinkOptions> options)
        {
            _webHostEnvironment = webHostEnvironment;
            _options = options;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Facebook = _options.Value.Facebook;
            ViewBag.Instagram = _options.Value.Instagram;
            ViewBag.YouTube = _options.Value.YouTube;
            ViewBag.Twitter = _options.Value.Twitter;
            return View();
        }
    }
}
