using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppExample.Models;
using StocksAppExample.Options;
using StocksAppExample.Services;
using System.Globalization;

namespace StocksAppExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;

        public HomeController(FinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "AAPL";
            }

            Dictionary<string, object>? responseDict = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDict["c"].ToString(),CultureInfo.InvariantCulture),
                LowestPrice = Convert.ToDouble(responseDict["l"].ToString(), CultureInfo.InvariantCulture),
                HighestPrice = Convert.ToDouble(responseDict["h"].ToString(), CultureInfo.InvariantCulture),
                OpenPrice = Convert.ToDouble(responseDict["o"].ToString(), CultureInfo.InvariantCulture),
            };

            return View(stock);
        }
    }
}
