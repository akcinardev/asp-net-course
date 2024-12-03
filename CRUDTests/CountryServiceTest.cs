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

            // Assert
            Assert.True(response.CountryID != Guid.Empty);
        }
    }
}
