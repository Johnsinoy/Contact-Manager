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

        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only alphabetic characters and spaces are allowed.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[A-Za-z\s'-]+$", ErrorMessage = "Only alphabetic characters, spaces, hyphens, and apostrophes are allowed.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format XXX-XXX-XXXX.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string Slug => (FirstName + "-" + LastName)?.ToLower().Replace(" ", "-");
    }
}


//