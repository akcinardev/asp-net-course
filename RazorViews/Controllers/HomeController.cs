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
                new Person() { Name = "omer", Birthday = Convert.ToDateTime("1998-07-10") , Gender = Gender.Male},
                new Person() { Name = "jill", Birthday = Convert.ToDateTime("1999-02-02") , Gender = Gender.Female},
                new Person() { Name = "claire", Birthday = Convert.ToDateTime("1998-01-10") , Gender = Gender.Female}
            };

            //ViewData["people"] = people;
            //ViewBag.people = people;

            return View(people);
        }

        [Route("details/{name}")]
        public IActionResult Details(string? name)
        {
            if(name == null)
            {
                return Content("Person name can not be null");
            }

            List<Person> people = new List<Person>() {
                new Person() { Name = "omer", Birthday = Convert.ToDateTime("1998-07-10") , Gender = Gender.Male},
                new Person() { Name = "jill", Birthday = Convert.ToDateTime("1999-02-02") , Gender = Gender.Female},
                new Person() { Name = "claire", Birthday = Convert.ToDateTime("1998-01-10") , Gender = Gender.Female}
            };

            Person matchingPerson = people.Where(person => person.Name == name).FirstOrDefault();

            return View(matchingPerson);
        }

        [Route("person-product")]
        public IActionResult PersonWithProduct()
        {
            Person person = new Person() { Name = "sara", Birthday = Convert.ToDateTime("1998-03-23"), Gender = Gender.Female };
            Product product = new Product() { Id = 1, Name = "Laptop" };
            PersonProductWrapper wrapper = new PersonProductWrapper() { PersonData = person, ProductData = product };
            return View(wrapper);
        }
    }
}
