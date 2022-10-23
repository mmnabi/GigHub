using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Attendance
    {
        public int GigId { get; set; }
        public Gig Gig { get; set; }

        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
    }
}
