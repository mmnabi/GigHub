namespace GigHub.Core.Resources
{
    public class GigResource
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Venue { get; set; }

        public GenreResource Genre { get; set; }

        public ArtistResource Artist { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}