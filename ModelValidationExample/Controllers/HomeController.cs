using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.CustomModelBinders;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //public IActionResult Index([FromBody] [ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        public IActionResult Index(Person person, [FromHeader(Name = "User-Agent")] string userAgent)
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

            return Content($"{person}, {userAgent}");
        }
    }
}
