using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models.ViewModel
{
    public class AppUserViewModel
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        public string? StudentNumber { get; set; }
    }
}
