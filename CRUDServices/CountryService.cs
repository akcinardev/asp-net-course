using CRUDEntities;
using CRUDServiceContracts;
using CRUDServiceContracts.DTO;

namespace CRUDServices
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>()
                {
                    new Country() { CountryID = Guid.Parse("D7374A6E-F929-4CFD-B42E-F6A43AFF61B2"), CountryName = "Turkey"},
                    new Country() { CountryID = Guid.Parse("16BB418F-1E1D-4E99-8E3A-0ED53BA8C133"), CountryName = "Germany"},
                    new Country() { CountryID = Guid.Parse("8F466413-5B58-49C7-AC7E-952C9A1BB947"), CountryName = "Netherlands"},
                    new Country() { CountryID = Guid.Parse("15976C2A-C6DB-4612-9B85-81A274560CB7"), CountryName = "Belgium"},
                    new Country() { CountryID = Guid.Parse("2EAB411E-72C0-4EFC-93D9-7D44BC45C16D"), CountryName = "France"}
                });
            }
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
            if(countryID == null) return null;

            Country? countryResponseFromGet = _countries.FirstOrDefault(c => c.CountryID == countryID);

            if(countryResponseFromGet == null) return null;

            return countryResponseFromGet.ToCountryResponse();
        }
    }
}
