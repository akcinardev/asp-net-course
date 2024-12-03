using CRUDServiceContracts.DTO;

namespace CRUDServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a Country object to the list of Country objects.
        /// </summary>
        /// <param name="countryAddRequest">Country object to add.</param>
        /// <returns>The Country object after adding it. (Includes newly generated Country ID.)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Gets all countries as a list.
        /// </summary>
        /// <returns>All countries as a list.</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Gets a Country object based on given CountryID.
        /// </summary>
        /// <param name="countryID">CountryID to search</param>
        /// <returns>Matching country as CountryResponse object.</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}
