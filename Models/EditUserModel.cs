using System.ComponentModel.DataAnnotations;

public class EditUserModel
{
    public string UserId { get; set; }

    [Required(ErrorMessage = "The First Name field is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Last Name field is required.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "The Email Address field is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "The UFS Student Number field is required.")]
    public string Identification { get; set; }
}