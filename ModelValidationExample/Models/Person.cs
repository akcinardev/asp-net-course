using ModelValidationExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} can not be empty.")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} chaaracters long.")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and dot.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "{0} should be a valid email address.")]
        [Required(ErrorMessage = "{0} can not be blank.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "{0} should be a valid phone number.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can not be blank.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} can not be blank.")]
        [Compare("Password",ErrorMessage = "{0} does not match with {1}")]
        [Display(Name = "Re-entered Password")]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between {1} and {2}.")]
        public double? Price { get; set; }

        [MinimumYearValidator(2000)]
        public DateTime? Birthday { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "From Date should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public List<string?> Tags { get; set; } = new List<string?>();

        public override string ToString()
        {
            return $"Person Object\nName: {Name}\nEmail: {Email}\nPhone: {Phone}\nPassword: {Password}\nConfirmPassword: {ConfirmPassword}\nPrice: {Price}\nBirthday: {Birthday}";
        }
    }
}
