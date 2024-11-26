using Microsoft.AspNetCore.Mvc;
using IActionResultExample.Models;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/bookstore/{bookid?}/{isloggedin?}")]
        public IActionResult Index([FromQuery]int? bookid, [FromRoute]bool isloggedin, Book book)
        // http://localhost:5210/bookstore/1/true?bookid=2&isloggedin=false
        {
            if (Convert.ToBoolean(isloggedin) == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be logged in for this action.");
                return Unauthorized("User must be logged in for this action.");
            }

            // Book id should be provided
            if (!bookid.HasValue)
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                return BadRequest("Book id is not supplied or invalid");
            }

            // Book id should not be less than 0
            if (bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id can not be null or empty!");
                return BadRequest("Book id can not be less than or equal to 0!");
            }

            // Book id should be in range 1-1000
            if (bookid > 1000)
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
