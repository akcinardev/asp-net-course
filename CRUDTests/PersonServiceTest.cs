using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using CRUDServices;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();
            _countryService = new CountryService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson

        // When you supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_PersonIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        // When you supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null};

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        // When you supply proper person details, it should insert the person into the persons list;
        // and it should return an object ofg PersonResponse,
        // which includes with the newly generated PersonID
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
                {
                    PersonName = "Omer",
                    Email = "example@email.com",
                    DateOfBirth = null,
                    Gender = GenderOptions.Male,
                    CountryID = Guid.NewGuid(),
                    Address = "Some Address",
                    ReceiveNewsLetters = false,
                };

            //Act
            PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);
            List<PersonResponse> personsList = _personService.GetAllPersons();

            //Assert
            Assert.True(personResponseFromAdd.PersonID != Guid.Empty);
            Assert.Contains(personResponseFromAdd, personsList);
        }

        #endregion

        #region GetPersonByPersonID

        [Fact]
        public void GetPersonByPersonID_PersonIdNull()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? personResponseFromGet = _personService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(personResponseFromGet);
        }

        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Turkey" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Omer",
                Email = "example@email.com",
                DateOfBirth = DateTime.Parse("1900-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "Some Address",
                ReceiveNewsLetters = false,
            };
            PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);
            PersonResponse? personResponseFromGet = _personService.GetPersonByPersonID(personResponseFromAdd.PersonID);

            //Act


            //Assert
            Assert.Equal(personResponseFromAdd, personResponseFromGet);
        }

        #endregion

        #region GetAllRegions

        //GetAllPersons should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> personsFromGet = _personService.GetAllPersons();

            //Assert
            Assert.Empty(personsFromGet);
        }

        //It should return all of the added persons
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest countryCodeAddRequest1 = new CountryAddRequest(){ CountryName = "Turkey" };
            CountryAddRequest countryCodeAddRequest2 = new CountryAddRequest(){ CountryName = "Germany" };

            CountryResponse countryResponse1 = _countryService.AddCountry(countryCodeAddRequest1);
            CountryResponse countryResponse2 = _countryService.AddCountry(countryCodeAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Omer",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID,
                Address = "Some Address",
                ReceiveNewsLetters = false,
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Claire",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info",
                ReceiveNewsLetters = true,
            };

            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info 2",
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2, personAddRequest3 };

            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();
            foreach(PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponseListFromAdd.Add(personResponse);
            }

            _testOutputHelper.WriteLine("Expected: ");
            foreach(PersonResponse personResponse in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Act
            List<PersonResponse> personsListFromGet = _personService.GetAllPersons();

            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse personResponse in personsListFromGet)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                Assert.Contains(personResponseFromAdd, personsListFromGet);
            }
        }

        #endregion
    }
}
