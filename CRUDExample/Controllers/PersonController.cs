using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

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
        public async Task<IActionResult> Index(
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
            List<PersonResponse> persons = await _personService.GetFilteredPersons(searchBy, searchString);


            // sorting
            List<PersonResponse> sortedPersons = await _personService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryID.ToString()});

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countryService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v=>v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

            return RedirectToAction("Index", "Person");
        }

        [Route("[action]/{personID}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(c => new SelectListItem() { Text = c.CountryName, Value = c.CountryID.ToString() });

            return View(personUpdateRequest);
        }

        [Route("[action]/{personID}")]
        [HttpPost]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = await _personService.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index", "Person");
            }
            else
            {
                List<CountryResponse> countries = await _countryService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
        }

        [Route("[action]/{personID}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid personID)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            return View(personResponse);
        }

        [Route("[action]/{personID}")]
        [HttpPost]
        public async Task<IActionResult> Delete(PersonUpdateRequest personUpdateResult)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personUpdateResult.PersonID);
            if (personResponse == null) return RedirectToAction("Index", "Person");

            await _personService.DeletePerson(personUpdateResult.PersonID);
            return RedirectToAction("Index", "Person");
        }

        [Route("[action]")]
        public async Task<IActionResult> PersonPDF()
        {
            // get list of persons
            List<PersonResponse> persons = await _personService.GetAllPersons();

            // return view as pdf
            return new ViewAsPdf("PersonPDF", persons, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20 , Bottom = 20, Right = 20, Left = 20},
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
            };
        }
    }
}
