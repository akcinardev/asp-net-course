using DIExampleServices;
using Microsoft.AspNetCore.Mvc;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly CitiesService _citiesService;

        // constructor
        public HomeController()
        {
            _citiesService = new CitiesService();
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
