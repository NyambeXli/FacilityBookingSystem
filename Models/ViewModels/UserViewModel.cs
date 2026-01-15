using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address field is required!")]
        [EmailAddress(ErrorMessage = "Please enter a valid  email address")]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password field is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? StudentNumber { get; set; }
        public string? StaffNumber { get; set; }
        public string? IDNumber { get; set; }
        public string? Identification { get; set; }
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; } = "/";
    }
    public class RegisterModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
    
        [Required(ErrorMessage = "Please enter your identification number.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The identification field only accepts numeric values.")]
        public string? Identification { get; set; }

        [Required(ErrorMessage = "Email address field is required!")]
        [EmailAddress(ErrorMessage = "Please enter a valid  email address")]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password field is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string? ConfirmPassword { get; set; }
    }
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Current Password field is required!")]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage = "Password field is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string? ConfirmPassword { get; set; }
    }
}
