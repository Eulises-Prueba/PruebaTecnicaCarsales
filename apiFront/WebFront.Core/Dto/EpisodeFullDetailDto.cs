namespace WebFront.Core.Dto
{
    public class EpisodeFullDetailDto
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string air_date { get; set; } = "";
        public string episode { get; set; } = "";
        public DateTime created { get; set; }
    }
}
