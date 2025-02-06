namespace WebFront.Core.Dto
{
    public class EpisodesDto
    {
        public EpisodeInfoDto info { get; set; }
        public List<EpisodeDetailDto> results { get; set; }

        public EpisodesDto()
        {
            info = new EpisodeInfoDto();
            results = new List<EpisodeDetailDto>();
        }
    }
}
