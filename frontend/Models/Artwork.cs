namespace WorldOfArt.Models
{
    public class Artwork
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }


        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
