using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServices;
using Microsoft.AspNetCore.Mvc;

namespace CRUDExample.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;

        public PersonController(IPersonService personService, ICountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;
        }

        [Route("/")]
        [Route("/persons/index")]
        public IActionResult Index()
        {
            List<PersonResponse> persons = _personService.GetAllPersons();

            return View(persons);
        }
    }
}
