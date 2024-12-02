using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IConfiguration _configuration;
        private readonly ExampleApiOptions _exampleApiOptions;

        //public HomeController(IConfiguration configuration)
        public HomeController(IOptions<ExampleApiOptions> exampleApiOptions)
        {
            _exampleApiOptions = exampleApiOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            // CONFIGURATION WITH GET()
            //ExampleApiOptions options = _configuration.GetSection("exampleapi").Get<ExampleApiOptions>();
            //ViewBag.ClientID = options.ClientID;
            //ViewBag.ClientSecret = options.ClientSecret;

            // CONFIGURATION WITH BIND()
            //ExampleApiOptions options = new ExampleApiOptions();
            //_configuration.GetSection("exampleapi").Bind(options);
            //ViewBag.ClientID = options.ClientID;
            //ViewBag.ClientSecret = options.ClientSecret;

            ViewBag.ClientID = _exampleApiOptions.ClientID;
            ViewBag.ClientSecret = _exampleApiOptions.ClientSecret;

            return View();
        }
    }
}
