using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using CRUDServices;

namespace CRUDTests
{
    public class CountryServiceTest
    {
        private readonly ICountryService _countryService;

        public CountryServiceTest()
        {
            _countryService = new CountryService();
        }

        #region AddCountry
        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_CountryAddRequestIsNull()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countryService.AddCountry(request);
            });
        }

        // When CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null};

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countryService.AddCountry(request);
            });
        }

        // When CountryName is duplicate, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "Turkey" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "Turkey" };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countryService.AddCountry(request1);
                _countryService.AddCountry(request2);
            });
        }

        // When proper CountryName supplied, it should insert the Country to the existing list of Countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Turkey" };

            // Act
            CountryResponse response = _countryService.AddCountry(request);
            List<CountryResponse> countryListFromGetAllCountries = _countryService.GetAllCountries();

            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countryListFromGetAllCountries);
        }
        #endregion

        #region GetAllCountries
        // The list of countries should be empty by default. (before adding any)
        [Fact]
        public void GetAllCountries_InitialListEmpty()
        {
            // Act
            List<CountryResponse> actualList = _countryService.GetAllCountries();

            // Assert
            Assert.Empty(actualList);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "Turkey"},
                new CountryAddRequest() { CountryName = "Germany"}
            };

            // Act
            List<CountryResponse> countriesListFromAddCountry = new List<CountryResponse>();

            foreach(CountryAddRequest addRequest in countryAddRequests)
            {
                countriesListFromAddCountry.Add(_countryService.AddCountry(addRequest));
            }

            // Assert
            List<CountryResponse> actualCountryResponseList = _countryService.GetAllCountries();
            foreach(CountryResponse expectedCountryResponse in countriesListFromAddCountry)
            {
                Assert.Contains(expectedCountryResponse, actualCountryResponseList);
            }
        }
        #endregion

        #region GetCountryByCountryID
        // If supplied CountryID is null, it should return null as CountryResponse
        [Fact]
        public void GetCountryByCountryID_CountryIDIsNull()
        {
            // Arrange
            Guid? countryID = null;

            // Act
            CountryResponse? countryResponseFromGetMethod = _countryService.GetCountryByCountryID(countryID);

            // Assert
            Assert.Null(countryResponseFromGetMethod);
        }

        // If supplied CountryID is valid, it should return matching country details as CountryResponse object
        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {
            // Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Turkey" };
            CountryResponse countryResponseFromAdd = _countryService.AddCountry(countryAddRequest);

            // Act
            CountryResponse? countryResponseFromGet = _countryService.GetCountryByCountryID(countryResponseFromAdd.CountryID);

            // Assert
            Assert.Equal(countryResponseFromAdd, countryResponseFromGet);
        }

        #endregion
    }
}
