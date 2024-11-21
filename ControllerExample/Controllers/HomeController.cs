using Microsoft.AspNetCore.Mvc;

namespace ControllerExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ContentResult Index()
        {
            ContentResult result = new ContentResult
            {
                Content = "<h1>Index Result</h1>",
                ContentType = "text/html"
            };
            return result;
        }

        [Route("/about")]
        public string About()
        {
            return "About Method";
        }

        [Route("/contact/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "Contact Method";
        }
    }
}
