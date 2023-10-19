using System.ComponentModel.DataAnnotations;

namespace SportsDataApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must meet specific criteria.")]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public string EmailVerificationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public User() { 
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            EmailVerificationToken = string.Empty;
            PasswordResetToken = string.Empty;
        }
    }
}
