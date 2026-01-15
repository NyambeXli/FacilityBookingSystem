using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models
{
    public class Library
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your surname.")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter your ID.")]
        public string LibraryName { get; set; }
        [Required(ErrorMessage = "Please enter the library name.")]
        public DateTime CheckInTime { get; set; }
        [Required(ErrorMessage = "Please enter the time of geting into the library.")]
        public DateTime CheckOutTime { get; set; }
        [Required(ErrorMessage = "Please enter the time of geting out of the library.")]
        public int NumberOfParticipants { get; set; }
        [Required(ErrorMessage = "Please enter the number of participants in the discussion.")]

        public int LibrayNameId { get; set; }
       
       

    }
}
