using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models.Entities
{
    public class Facility
    {

        [Key]
        public int FacilityId { get; set; }
        [Required]
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public Catagory? Category { get; set; }
    }
}
