using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using CRUDServices.Helpers;
using System.Reflection;

namespace CRUDServices
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;

        public PersonService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countryService = new CountryService();

            if(initialize)
            {
                _persons.AddRange(new List<Person>()
                {
                    new Person()
                    {
                        PersonID = Guid.Parse("A29D1F74-1DFF-4907-AE13-3A95C2F8A4B1"),
                        PersonName = "John Doe",
                        Email = "johndoe@example.com",
                        DateOfBirth = DateTime.Parse("1985-04-12"),
                        Gender = "Male",
                        CountryID = Guid.Parse("D7374A6E-F929-4CFD-B42E-F6A43AFF61B2"),
                        Address = "123 Elm Street, Istanbul",
                        ReceiveNewsLetters = true
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("E7B8C3C2-5FAE-462D-8D19-36CDBA7F6474"),
                        PersonName = "Jane Smith",
                        Email = "janesmith@example.com",
                        DateOfBirth = DateTime.Parse("1992-07-08"),
                        Gender = "Female",
                        CountryID = Guid.Parse("16BB418F-1E1D-4E99-8E3A-0ED53BA8C133"),
                        Address = "45 Oak Street, Berlin",
                        ReceiveNewsLetters = false
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("9E17E962-B74E-4621-9B2E-15D2312A5B6F"),
                        PersonName = "Michael Brown",
                        Email = "michaelbrown@example.com",
                        DateOfBirth = DateTime.Parse("1980-11-03"),
                        Gender = "Male",
                        CountryID = Guid.Parse("8F466413-5B58-49C7-AC7E-952C9A1BB947"),
                        Address = "12 Pine Avenue, Amsterdam",
                        ReceiveNewsLetters = true
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("F4C1A6B2-91B4-45F2-B97D-A7EE4F2C4471"),
                        PersonName = "Emily Davis",
                        Email = "emilydavis@example.com",
                        DateOfBirth = DateTime.Parse("1995-02-20"),
                        Gender = "Female",
                        CountryID = Guid.Parse("15976C2A-C6DB-4612-9B85-81A274560CB7"),
                        Address = "56 Maple Lane, Brussels",
                        ReceiveNewsLetters = false
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("34A719B9-207D-42F9-9B5D-8F3A8B6DA2D6"),
                        PersonName = "David Wilson",
                        Email = "davidwilson@example.com",
                        DateOfBirth = DateTime.Parse("1978-09-15"),
                        Gender = "Male",
                        CountryID = Guid.Parse("2EAB411E-72C0-4EFC-93D9-7D44BC45C16D"),
                        Address = "789 Birch Road, Paris",
                        ReceiveNewsLetters = true
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("7B3E1942-CCAD-4A3C-B9A4-D412B2A54D1E"),
                        PersonName = "Sophia Martinez",
                        Email = "sophiamartinez@example.com",
                        DateOfBirth = DateTime.Parse("1987-06-30"),
                        Gender = "Female",
                        CountryID = Guid.Parse("16BB418F-1E1D-4E99-8E3A-0ED53BA8C133"),
                        Address = "101 Walnut Drive, Hamburg",
                        ReceiveNewsLetters = false
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("3A4ED762-5F1B-4C5A-AF49-1E282B892C5E"),
                        PersonName = "Christopher Taylor",
                        Email = "christay@example.com",
                        DateOfBirth = DateTime.Parse("1990-12-21"),
                        Gender = "Male",
                        CountryID = Guid.Parse("8F466413-5B58-49C7-AC7E-952C9A1BB947"),
                        Address = "21 Cypress Court, Rotterdam",
                        ReceiveNewsLetters = true
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("EC7F23A1-FF9D-4E98-8662-3B9E321ADF46"),
                        PersonName = "Olivia Johnson",
                        Email = "oliviajohnson@example.com",
                        DateOfBirth = DateTime.Parse("2000-01-01"),
                        Gender = "Female",
                        CountryID = Guid.Parse("D7374A6E-F929-4CFD-B42E-F6A43AFF61B2"),
                        Address = "67 Chestnut Boulevard, Ankara",
                        ReceiveNewsLetters = true
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("15A217DE-9DC9-47BC-9206-7BB639ABEAD5"),
                        PersonName = "James Anderson",
                        Email = "jamesanderson@example.com",
                        DateOfBirth = DateTime.Parse("1982-03-11"),
                        Gender = "Male",
                        CountryID = Guid.Parse("15976C2A-C6DB-4612-9B85-81A274560CB7"),
                        Address = "10 Beechwood Circle, Ghent",
                        ReceiveNewsLetters = false
                    },
                    new Person()
                    {
                        PersonID = Guid.Parse("DD8A74C9-F7A1-4F48-9E38-479B27B21E5D"),
                        PersonName = "Ava Thompson",
                        Email = "avathompson@example.com",
                        DateOfBirth = DateTime.Parse("1993-10-05"),
                        Gender = "Female",
                        CountryID = Guid.Parse("2EAB411E-72C0-4EFC-93D9-7D44BC45C16D"),
                        Address = "37 Spruce Avenue, Lyon",
                        ReceiveNewsLetters = true
                    }
                });
            }
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
            _persons.Add(person);

            // Convert the Person into PersonResponse and return with PersonID
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if( personID == null) return null;

            Person? person = _persons.FirstOrDefault(p =>  p.PersonID == personID);

            if (person == null) return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            // Get all persons into a list
            List<PersonResponse> allPersons = GetAllPersons();

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

        //public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        //{
        //    if(string.IsNullOrEmpty(sortBy)) return allPersons;

        //    List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
        //    {
        //        (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
        //        (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

        //        (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),
        //        (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

        //        (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
        //        (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),

        //        (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Age).ToList(),
        //        (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Age).ToList(),

        //        (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
        //        (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

        //        (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),
        //        (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

        //        (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),
        //        (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

        //        (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.ReceiveNewsLetters).ToList(),
        //        (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.ReceiveNewsLetters).ToList(),

        //        _ => allPersons
        //    };

        //    return sortedPersons;
        //}

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder) // GetSortedPersons LOGIC WITH REFLECTIONS
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

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null) throw new ArgumentNullException(nameof(personUpdateRequest));

            // validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            // get matching person obj for updating and check for null
            Person? matchingPerson = _persons.FirstOrDefault(p => p.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null) throw new ArgumentException("Given person ID does not exist.");

            // update details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null) throw new ArgumentNullException(nameof(personID));

            Person? matchingPerson = _persons.FirstOrDefault(p => p.PersonID == personID);

            if (matchingPerson == null) return false;

            _persons.RemoveAll(p => p.PersonID == personID);

            return true;
        }
    }
}
