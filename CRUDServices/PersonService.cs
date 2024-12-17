using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using CRUDServices.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CRUDServices
{
    public class PersonService : IPersonService
    {
        private readonly PersonsDbContext _db;
        private readonly ICountryService _countryService;

        public PersonService (PersonsDbContext personsDbContext, ICountryService countryService)
        {
            _db = personsDbContext;
            _countryService = countryService;
        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
            // Check if "personAddRequest" is null
            if(personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));

            // Validate all properties of "personAddRequest"
            //if(string.IsNullOrEmpty(personAddRequest.PersonName)) throw new ArgumentException("PersonName can not be blank.");
            //ValidationContext validationContext = new ValidationContext(personAddRequest);
            //List<ValidationResult> validationResults= new List<ValidationResult>();
            //bool isValid = Validator.TryValidateObject(personAddRequest, validationContext, validationResults, true);

            //if (!isValid) throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            ValidationHelper.ModelValidation(personAddRequest);

            // Convert personAddRequest from PersonAddRequest type to Person
            Person person = personAddRequest.ToPerson();

            // Generate a new PersonID
            person.PersonID = Guid.NewGuid();

            // Add it to the List<Person>
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();
            //_db.sp_InsertPerson(person); // STORED PROCEDURE

            // Convert the Person into PersonResponse and return with PersonID
            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            // Tries to translate the method and gives error. Dont use classes or methods in LINQ
            //return _db.Persons.Select(p => ConvertPersonToPersonResponse(p)).ToList();

            // First gets the data from db as a list and then we can implement convert method on that
            // not directly inside of LINQ query
            var persons = await _db.Persons.Include("Country").ToListAsync();

            return persons.Select(p => p.ToPersonResponse()).ToList();

            // with stored procedures
            //return _db.sp_GetAllPersons().Select(p => ConvertPersonToPersonResponse(p)).ToList();
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if( personID == null) return null;

            Person? person = await _db.Persons.Include("Country").FirstOrDefaultAsync(p =>  p.PersonID == personID);

            if (person == null) return null;

            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            // Get all persons into a list
            List<PersonResponse> allPersons = await GetAllPersons();

            // Assign all persons to the matchingPersons as default
            List<PersonResponse> matchingPersons = allPersons;

            // If the searchBy or searchString is null or empty str return allPersons as default
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString)) return matchingPersons;

            // SearchBy cases
            switch (searchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.PersonName) ?   // If the PersonName prop is not null or empty string
                        p.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) :   // Look for searchString in PersonName
                        true).ToList();                                                             // If the PersonName prop is null or empty string match it
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.Email) ?
                        p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(p => (p.DateOfBirth != null) ?
                        p.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.Gender) ?
                        p.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.Country) ?
                        p.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.Address) ?
                        p.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default: matchingPersons = allPersons;
                    break;
            }

            return matchingPersons;
        }

        public async Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder) // GetSortedPersons LOGIC WITH REFLECTIONS
        {
            if (string.IsNullOrEmpty(sortBy)) return allPersons;

            var propertyInfo = typeof(PersonResponse).GetProperty(sortBy, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (propertyInfo == null) throw new ArgumentException($"Invalid sortBy parameter: {sortBy}");

            IOrderedEnumerable<PersonResponse> sortedPersons = sortOrder switch
            {
                SortOrderOptions.ASC => allPersons.OrderBy(p => propertyInfo.GetValue(p, null)),
                SortOrderOptions.DESC => allPersons.OrderByDescending(p => propertyInfo.GetValue(p, null)),
                _ => throw new ArgumentException($"Invalid sortOrder parameter: {sortOrder}")
            };

            return sortedPersons.ToList();
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null) throw new ArgumentNullException(nameof(personUpdateRequest));

            // validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            // get matching person obj for updating and check for null
            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(p => p.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null) throw new ArgumentException("Given person ID does not exist.");

            // update details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            await _db.SaveChangesAsync();

            return matchingPerson.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null) throw new ArgumentNullException(nameof(personID));

            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(p => p.PersonID == personID);

            if (matchingPerson == null) return false;

            _db.Persons.Remove(matchingPerson);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}