using WebFront.Core.Domain;

namespace WebFront.Core.Interface
{
    public interface IRickAndMortyApiClient
    {
        public Task<Episodes> GetEpisodes(int? page);
        public Task<EpisodeDetail> GetEpisodeDetail(int id);
    }
}
