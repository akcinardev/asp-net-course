using CRUDEntities;

namespace CRUDServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of CountryService methods.
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        // It compares the current object to another object of CountryResponse
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse countryToCompare = (CountryResponse)obj;

            return this.CountryID == countryToCompare.CountryID && this.CountryName == countryToCompare.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}
