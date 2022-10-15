using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        // public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        [StringLength(450)]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

    }
}
