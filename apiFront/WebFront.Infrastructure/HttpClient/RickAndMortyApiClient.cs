using WebFront.Core.Domain;
using WebFront.Core.Interface;

using System.Text.Json;
using Polly.Registry;

namespace WebFront.Infrastructure.HttpClient
{
    public class RickAndMortyApiClient(IHttpClientFactory httpClientFactory, ResiliencePipelineProvider<string> resiliencePipelineProvider) : IRickAndMortyApiClient
    {
        public async Task<Episodes> GetEpisodes(int? page)
        {
            var result = new Episodes();
            using var client = httpClientFactory.CreateClient("RickAndMortyApi");

            var sGet = page > 0 ? $"?page={page}" : "";
            var pipeline = resiliencePipelineProvider.GetPipeline("default");
            var response = await pipeline.ExecuteAsync(
                async ct => await client.GetAsync($"/api/episode{sGet}", ct)
            );
            if (!response.IsSuccessStatusCode) return result;

            var sData = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<Episodes>(sData)!;
            return result;
        }

        public async Task<EpisodeDetail> GetEpisodeDetail(int id)
        {
            var result = new EpisodeDetail();
            using var client = httpClientFactory.CreateClient("RickAndMortyApi");

            var pipeline = resiliencePipelineProvider.GetPipeline("default");
            var response = await pipeline.ExecuteAsync(
                async ct => await client.GetAsync($"/api/episode/{id}", ct)
            );
            if (!response.IsSuccessStatusCode) return result;

            var sData = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<EpisodeDetail>(sData)!;
            return result;
        }
    }
}
