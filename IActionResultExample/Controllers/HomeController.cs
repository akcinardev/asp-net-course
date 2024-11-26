using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/bookstore")]
        public IActionResult Index()
        // http://localhost:5210/bookstore?bookid=1&isloggedin=true
        {
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be logged in for this action.");
                return Unauthorized("User must be logged in for this action.");
            }

            // Book id should be provided
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                return BadRequest("Book id is not supplied");
            }

            // Book id should not be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id can not be null or empty!");
                return BadRequest("Book id can not be null or empty!");
            }

            // Book id should be in range 1-1000
            int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookid"]);
            if(bookId <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id can not be less than or equal to 0");
                return BadRequest("Book id can not be less than or equal to 0");
            }

            if (bookId > 1000)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can not be greater than 1000");
                return NotFound("Book id can not be greater than 1000");
            }

            //return File("sample.pdf", "application/pdf");

            // REDIRECTS TO AN ACTION IN A CONTROLLER
            //return new RedirectToActionResult("Books", "Store", new {}); // 302 - Found
            //return RedirectToAction("Books", "Store", new { id = bookId }); // 302 - Found

            //return new RedirectToActionResult("Books", "Store", new {}, true); // 301 - Moved Permanently
            //return RedirectToActionPermanent("Books", "Store", new {}); // 301 - Moved Permanently


            // REDIRECTS TO A SPECIFIC URL WITHIN APP
            //return new LocalRedirectResult($"store/books/{bookId}"); // Directly to specific URL within app // 302 - Found
            //return LocalRedirect($"store/books/{bookId}"); // Directly to specific URL within app // 302 - Found

            //return new LocalRedirectResult($"store/books/{bookId}", true); // Directly to specific URL within app // 301 - Moved Permanently
            //return LocalRedirectPermanent($"store/books/{bookId}"); // Directly to specific URL within app // 301 - Moved Permanently


            // REDIRECTS TO A SPECIFIC URL
            return Redirect("https://google.com");
        }
    }
}
