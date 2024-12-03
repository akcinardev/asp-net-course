using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;

namespace CRUDServices
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validate: countryAddRequest can not be null
            if (countryAddRequest == null) throw new ArgumentNullException(nameof(countryAddRequest));

            // Validate: CountryName can not be null
            if(countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validate: CountryName can not be duplicate
            if (_countries.Where(c => c.CountryName == countryAddRequest.CountryName).Count() > 0) throw new ArgumentException("Given Country Name is already exists and can not be duplicate.");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            throw new NotImplementedException();
        }
    }
}
