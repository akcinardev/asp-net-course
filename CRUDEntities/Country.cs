using System.ComponentModel.DataAnnotations;

namespace CRUDEntities
{
    /// <summary>
    /// Domain model for Country.
    /// </summary>
    public class Country
    {
        [Key]
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
        public virtual ICollection<Person>? Persons { get; set; }
    }
}
