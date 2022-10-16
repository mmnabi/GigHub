using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Resources
{
    public class SaveGigResource
    {
        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}