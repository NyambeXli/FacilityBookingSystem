using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models.Entities
{
    public class Catagory
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; } = default(decimal?);
        public ICollection<Facility>? Facilities { get; set; }
    }
}
