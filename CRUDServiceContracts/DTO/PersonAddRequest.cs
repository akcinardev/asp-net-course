using CRUDEntities;
using CRUDServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRUDServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new Person object.
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person name can not be blank.")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can not be blank.")]
        [EmailAddress(ErrorMessage = "Email address should be a valid email address.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts current object of PersonAddRequest to Person class.
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person()
            {
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
