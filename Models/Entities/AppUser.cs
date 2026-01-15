using Microsoft.AspNetCore.Identity;

namespace UfsConnectBook.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Identity { get; set; }
    }
}
