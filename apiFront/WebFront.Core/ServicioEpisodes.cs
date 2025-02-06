using WebFront.Core.Dto;
using WebFront.Core.Helper;
using WebFront.Core.Interface;

namespace WebFront.Core
{
    public class ServicioEpisodes(IRickAndMortyApiClient apiRickAndMorty) : IConsultasEpisode
    {
        public async Task<EpisodesDto> GetLista(int? page)
        {
            var result = await apiRickAndMorty.GetEpisodes(page);
            return result.ConvertWithJson<EpisodesDto>()!;
        }

        public async Task<EpisodeFullDetailDto> GetDetalle(int id)
        {
            var result = await apiRickAndMorty.GetEpisodeDetail(id);
            return result.ConvertWithJson<EpisodeFullDetailDto>()!;
        }
    }
}
