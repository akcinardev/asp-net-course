using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;
using CRUDServices;
using Microsoft.EntityFrameworkCore;
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
            _countryService = new CountryService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
            _personService = new PersonService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options), _countryService);
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

        #region GetFilteredPersons

        //If the search text is empty and search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange
            CountryAddRequest countryCodeAddRequest1 = new CountryAddRequest() { CountryName = "Turkey" };
            CountryAddRequest countryCodeAddRequest2 = new CountryAddRequest() { CountryName = "Germany" };

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
            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponseListFromAdd.Add(personResponse);
            }

            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse personResponse in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Act
            List<PersonResponse> personsListFromSearch = _personService.GetFilteredPersons(nameof(Person.PersonName), "");

            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse personResponse in personsListFromSearch)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                Assert.Contains(personResponseFromAdd, personsListFromSearch);
            }
        }

        // First we will add few persons and search will be conducted based on search string
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange
            CountryAddRequest countryCodeAddRequest1 = new CountryAddRequest() { CountryName = "Turkey" };
            CountryAddRequest countryCodeAddRequest2 = new CountryAddRequest() { CountryName = "Germany" };

            CountryResponse countryResponse1 = _countryService.AddCountry(countryCodeAddRequest1);
            CountryResponse countryResponse2 = _countryService.AddCountry(countryCodeAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID,
                Address = "Some Address",
                ReceiveNewsLetters = false,
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Dutch van der Linde",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info",
                ReceiveNewsLetters = true,
            };

            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "John Marston",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info 2",
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2, personAddRequest3 };

            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponseListFromAdd.Add(personResponse);
            }

            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse personResponse in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            string SEARCH_STR = "ar";

            //Act
            List<PersonResponse> personsListFromSearch = _personService.GetFilteredPersons(nameof(Person.PersonName), SEARCH_STR);

            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse personResponse in personsListFromSearch)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                if(personResponseFromAdd.PersonName != null)
                {
                    if (personResponseFromAdd.PersonName.Contains(SEARCH_STR, StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponseFromAdd, personsListFromSearch);
                    }
                }
            }
        }

        #endregion

        #region GetSortedPersons

        // When we sort based on PersonName in DESC, it should return sorted list descending (z..a, 9..0)
        [Fact]
        public void GetSortedPersons_SortByPersonNameDESC()
        {
            //Arrange
            CountryAddRequest countryCodeAddRequest1 = new CountryAddRequest() { CountryName = "Turkey" };
            CountryAddRequest countryCodeAddRequest2 = new CountryAddRequest() { CountryName = "Germany" };

            CountryResponse countryResponse1 = _countryService.AddCountry(countryCodeAddRequest1);
            CountryResponse countryResponse2 = _countryService.AddCountry(countryCodeAddRequest2);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Dutch van der Linde",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID,
                Address = "Some Address",
                ReceiveNewsLetters = false,
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "John Marston",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Female,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info",
                ReceiveNewsLetters = true,
            };

            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse2.CountryID,
                Address = "Address Info 2",
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2, personAddRequest3 };

            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            foreach (PersonAddRequest personRequest in personRequests)
            {
                PersonResponse personResponse = _personService.AddPerson(personRequest);
                personResponseListFromAdd.Add(personResponse);
            }

            List<PersonResponse> allPersons = _personService.GetAllPersons();

            //Act
            List<PersonResponse> personsListSorted = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            personResponseListFromAdd = personResponseListFromAdd.OrderByDescending(p => p.PersonName).ToList();

            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse personResponse in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse personResponse in personsListSorted)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            //Assert
            for (int i = 0; i < personResponseListFromAdd.Count; i++)
            {
                Assert.Equal(personResponseListFromAdd[i], personsListSorted[i]);
            }
        }

        #endregion

        #region UpdatePerson

        // when you supply null value as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_PersonUpdateRequestIsNull()
        {
            // arrange
            PersonUpdateRequest? personUpdateRequest = null;

            // assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // act
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        // when you supply invalid value as PersonID, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonIDIsInvalid()
        {
            // arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { PersonID = Guid.NewGuid() };

            // assert
            Assert.Throws<ArgumentException>(() =>
            {
                // act
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        // when PersonName is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() {CountryName = "Blackwater"};
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "Blackwater Village",
                ReceiveNewsLetters = false,
            };

            PersonResponse personAddResponse = _personService.AddPerson(personAddRequest);

            PersonUpdateRequest? personUpdateRequest = personAddResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            // assert
            Assert.Throws<ArgumentException>(() =>
            {
                // act
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        // when PersonName is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdate()
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Blackwater" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "Blackwater Village",
                ReceiveNewsLetters = false,
            };

            PersonResponse personAddResponse = _personService.AddPerson(personAddRequest);

            PersonUpdateRequest? personUpdateRequest = personAddResponse.ToPersonUpdateRequest();

            personUpdateRequest.PersonName = "John Marston";
            personUpdateRequest.Address = "Strawberry Village";

            // act
            PersonResponse personResponseFromUpdate = _personService.UpdatePerson(personUpdateRequest);
            PersonResponse? personResponseAfterUpdate = _personService.GetPersonByPersonID(personUpdateRequest.PersonID);

            // assert
            Assert.Equal(personResponseAfterUpdate, personResponseFromUpdate);
        }

        #endregion

        #region DeletePerson

        // if you supply VALID PersonID, it should return TRUE
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Blackwater" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "Blackwater Village",
                ReceiveNewsLetters = false,
            };

            PersonResponse personAddResponse = _personService.AddPerson(personAddRequest);

            // act
            bool isDeleted = _personService.DeletePerson(personAddResponse.PersonID);

            // assert
            Assert.True(isDeleted);
        }

        // if you supply INVALID PersonID, it should return FALSE
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Blackwater" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Arthur Morgan",
                Email = "example@email.com",
                DateOfBirth = null,
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "Blackwater Village",
                ReceiveNewsLetters = false,
            };

            PersonResponse personAddResponse = _personService.AddPerson(personAddRequest);

            // act
            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            // assert
            Assert.False(isDeleted);
        }

        #endregion
    }
}
