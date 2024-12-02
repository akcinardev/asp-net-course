using Microsoft.AspNetCore.Mvc;
using StocksAppExample.Services;

namespace StocksAppExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubService;

        public HomeController(FinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? responseDict = await _finnhubService.GetStockPriceQuote("APPL");
            return View();
        }
    }
}
