using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;

namespace CRUDServices
{
    public class CountryService : ICountryService
    {
        private readonly PersonsDbContext _db;

        public CountryService(PersonsDbContext personsDbContext)
        {
            _db = personsDbContext;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validate: countryAddRequest can not be null
            if (countryAddRequest == null) throw new ArgumentNullException(nameof(countryAddRequest));

            // Validate: CountryName can not be null
            if(countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validate: CountryName can not be duplicate
            if (await _db.Countries.CountAsync(c => c.CountryName == countryAddRequest.CountryName) > 0) throw new ArgumentException("Given Country Name is already exists and can not be duplicate.");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null) return null;

            Country? countryResponseFromGet = await _db.Countries.FirstOrDefaultAsync(c => c.CountryID == countryID);

            if(countryResponseFromGet == null) return null;

            return countryResponseFromGet.ToCountryResponse();
        }
    }
}
