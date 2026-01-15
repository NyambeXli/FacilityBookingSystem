using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UfsConnectBook.Models
{
    public class User
    {


        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(50)]
        [Range(2, 50, ErrorMessage = "Name must be between 2 to 50 character")]
        public string Name { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "Please enter your surname")]
        [StringLength(50)]
        [Range(2, 50, ErrorMessage = "Name must be between 2 to 50 character")]
        public string Surname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter email adress")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Staff number")]
        [Required(ErrorMessage = "Please enter staff number")]
        public string StaffNumber { get; set; }

        [DisplayName("Student Number")]
        [Required(ErrorMessage = "Please enter student number number")]
        public string StudentNumnber { get; set; }

        [DisplayName("Passport/ID")]
        [Required(ErrorMessage = "Please enter your passport/ID")]
        [Range(1, 13, ErrorMessage = "The ID/Passport must be 13 charachters")]
        public int IDPassport { get; set; }



    }
}
