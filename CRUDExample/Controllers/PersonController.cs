using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDExample.Controllers
{
    [Route("[controller]")]
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
        [Route("[action]")]
        public IActionResult Index(
            string searchBy,
            string? searchString,
            string sortBy = nameof(PersonResponse.PersonName),
            SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            // searching
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name" },
                {nameof(PersonResponse.Email), "Email" },
                {nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                {nameof(PersonResponse.Gender), "Gender" },
                {nameof(PersonResponse.CountryID), "Country" },
                {nameof(PersonResponse.Address), "Address" },
            };

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //List<PersonResponse> persons = _personService.GetAllPersons();
            List<PersonResponse> persons = _personService.GetFilteredPersons(searchBy, searchString);


            // sorting
            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryID.ToString()});

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countryService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v=>v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            return RedirectToAction("Index", "Person");
        }

        [Route("[action]/{personID}")]
        [HttpGet]
        public IActionResult Edit(Guid personID)
        {
            PersonResponse? personResponse = _personService.GetPersonByPersonID(personID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            List<CountryResponse> countries = _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryID.ToString() });

            return View(personUpdateRequest);
        }

        [Route("[action]/{personID}")]
        [HttpPost]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personService.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = _personService.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index", "Person");
            }
            else
            {
                List<CountryResponse> countries = _countryService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
        }

        [Route("[action]/{personID}")]
        [HttpGet]
        public IActionResult Delete(Guid personID)
        {
            PersonResponse? personResponse = _personService.GetPersonByPersonID(personID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            return View(personResponse);
        }

        [Route("[action]/{personID}")]
        [HttpPost]
        public IActionResult Delete(PersonUpdateRequest personUpdateResult)
        {
            PersonResponse? personResponse = _personService.GetPersonByPersonID(personUpdateResult.PersonID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            _personService.DeletePerson(personUpdateResult.PersonID);
            return RedirectToAction("Index", "Person");
        }
    }
}
