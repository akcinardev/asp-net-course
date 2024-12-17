using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDEntities
{
    /// <summary>
    /// Domain model for Person.
    /// </summary>
    public class Person
    {
        [Key]
        //[Required]
        public Guid PersonID { get; set; }

        [StringLength(40)] //nvarchar(40)
        public string? PersonName { get; set; }

        [StringLength(40)] //nvarchar(40)
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(6)] //nvarchar(40)
        public string? Gender { get; set; }

        //uniqueidentifier
        public Guid? CountryID { get; set; }

        [StringLength(200)] //nvarchar(40)
        public string? Address { get; set; }

        //bit
        public bool ReceiveNewsLetters { get; set; }

        public string? TIN { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country? Country { get; set; }
    }
}
