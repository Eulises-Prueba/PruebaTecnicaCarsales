namespace WebFront.Core.Domain
{
    public class Episodes
    {
        public EpisodeInfo info { get; set; }
        public List<EpisodeDetail> results { get; set; }

        public Episodes()
        {
            info = new EpisodeInfo();
            results = new List<EpisodeDetail>();
        }
    }
}
