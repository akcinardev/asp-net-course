using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        public IActionResult Index(Person person)
        {
            if(!ModelState.IsValid)
            {
                //List<string> errorsList = new List<string>();
                //List<string> errorList = new List<string>();
                //foreach(var value in ModelState.Values)
                //{
                //    foreach (var error in value.Errors)
                //    {
                //        errorList.Add(error.ErrorMessage);
                //    }
                //}
                List<string> errorsList = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                string errors = string.Join("\n", errorsList);

                return BadRequest(errors);
            }

            return Content($"{person}");
        }
    }
}
