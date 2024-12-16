using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;

namespace CRUDServices
{
    public class CountryService : ICountryService
    {
        private readonly PersonsDbContext _db;

        public CountryService(PersonsDbContext personsDbContext)
        {
            _db = personsDbContext;
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validate: countryAddRequest can not be null
            if (countryAddRequest == null) throw new ArgumentNullException(nameof(countryAddRequest));

            // Validate: CountryName can not be null
            if(countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validate: CountryName can not be duplicate
            if (_db.Countries.Count(c => c.CountryName == countryAddRequest.CountryName) > 0) throw new ArgumentException("Given Country Name is already exists and can not be duplicate.");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _db.Countries.Add(country);
            _db.SaveChanges();

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _db.Countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null) return null;

            Country? countryResponseFromGet = _db.Countries.FirstOrDefault(c => c.CountryID == countryID);

            if(countryResponseFromGet == null) return null;

            return countryResponseFromGet.ToCountryResponse();
        }
    }
}
