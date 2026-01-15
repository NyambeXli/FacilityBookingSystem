using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UfsConnectBook.Models
{
    public class Facilityv1
    {

        [DisplayName("Facility ID")]
        [Required(ErrorMessage = "Please enter facility Id")]
        public int FacilityId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter facility name")]
        public string FacilityName { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter the specified facility description")]
        [StringLength(50)]
        [Range(2, 200, ErrorMessage = "description must be between 2 to 500 character")]
        public string Surname { get; set; }

        [DisplayName("Building name")]
        [Required(ErrorMessage = "Please enter the building where the facility is located ")]
        public string BuildingName { get; set; }

        [DisplayName("Check In time")]
        [Required(ErrorMessage = "Provide the time you will use the facility")]
        public DateTime CheckInTime { get; set; }

        [DisplayName("Check out time")]
        [Required(ErrorMessage = "Provide the time you will leave the facility")]
        public DateTime CheckOutTime { get; set; }

    }
}
