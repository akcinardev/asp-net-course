using CRUDEntities;
using CRUDServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRUDServiceContracts.DTO
{
    /// <summary>
    /// Represents the DTO to update Person object.
    /// </summary>
    public class PersonUpdateRequest
    {
        [Required]
        public Guid PersonID { get; set; }

        [Required(ErrorMessage = "Person name can not be blank.")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can not be blank.")]
        [EmailAddress(ErrorMessage = "Email address should be a valid email address.")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts current object of PersonAddRequest to Person class.
        /// </summary>
        /// <returns>Updated Person object.</returns>
        public Person ToPerson()
        {
            return new Person()
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }
}
