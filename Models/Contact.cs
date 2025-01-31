using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format XXX-XXX-XXXX")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
    }
}
