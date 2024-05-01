using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Api.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "{0} field is required")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} field is required")]
        [StringLength(100, ErrorMessage = "{0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "{0} field is required")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} field is required")]
        [StringLength(100, ErrorMessage = "{0} field must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserTokenViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
