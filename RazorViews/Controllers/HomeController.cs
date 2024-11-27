using Microsoft.AspNetCore.Mvc;
using RazorViews.Models;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("home")]
        public IActionResult Index()
        {
            ViewData["pageTitle"] = "Razor Views Example";

            List<Person> people = new List<Person>() {
                new Person() { Name = "Omer", Birthday = Convert.ToDateTime("1998-07-10") , Gender = Gender.Male},
                new Person() { Name = "Beyza", Birthday = Convert.ToDateTime("1999-02-11") , Gender = Gender.Female},
                new Person() { Name = "Isra", Birthday = Convert.ToDateTime("1998-06-15") , Gender = Gender.Female}
            };

            ViewData["people"] = people;

            return View();
        }
    }
}
