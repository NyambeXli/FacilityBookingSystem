using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UfsConnectBook.Models.Entities
{
    public class Booking
    {
        [Key] public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan? Duration { get; set; } = TimeSpan.FromSeconds(1);
        [Display(Name = "Facility")]
        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Status { get; set; }
        [ProtectedPersonalData]
        public string? userEmail { get; set; }
    }
}
