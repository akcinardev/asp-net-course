using DIExampleContracts;
using DIExampleServices;
using Microsoft.AspNetCore.Mvc;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;

        public HomeController(ICitiesService citiesService, ICitiesService citiesService2, ICitiesService citiesService3)
        {
            _citiesService = citiesService;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            ViewBag.InstanceId_Srv1 = _citiesService.InstanceId;
            ViewBag.InstanceId_Srv2 = _citiesService2.InstanceId;
            ViewBag.InstanceId_Srv3 = _citiesService3.InstanceId;
            return View(cities);
        }
    }
}
