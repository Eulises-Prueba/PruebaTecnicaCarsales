using WebFront.Core.Dto;

namespace WebFront.Core.Interface
{
    public interface IConsultasEpisode
    {
        public Task<EpisodesDto> GetLista(int? page);
        public Task<EpisodeFullDetailDto> GetDetalle(int id);
    }
}
