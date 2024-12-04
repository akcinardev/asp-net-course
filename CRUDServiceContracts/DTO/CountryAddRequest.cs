using CRUDEntities;

namespace CRUDServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new Country object.
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country { CountryName = CountryName };
        }
    }
}
