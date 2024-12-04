using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;

namespace CRUDServices
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;

        public PersonService()
        {
            _persons = new List<Person>();
            _countryService = new CountryService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            // Check if "personAddRequest" is null
            if(personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));

            // Validate all properties of "personAddRequest"
            if(string.IsNullOrEmpty(personAddRequest.PersonName)) throw new ArgumentException("PersonName can not be blank.");

            // Convert personAddRequest from PersonAddRequest type to Person
            Person person = personAddRequest.ToPerson();

            // Generate a new PersonID
            person.PersonID = Guid.NewGuid();

            // Add it to the List<Person>
            _persons.Add(person);

            // Convert the Person into PersonResponse and return with PersonID
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
