using NSubstitute;
using WebFront.Core.Domain;
using WebFront.Core.Dto;
using WebFront.Core.Interface;

namespace WebFront.Core.UnitTests;

public class ServicioEpisodesTests
{
    internal IRickAndMortyApiClient Client;

    public ServicioEpisodesTests()
    {
        Client = Substitute.For<IRickAndMortyApiClient>();

        var mokList = new Episodes
        {
            info = new EpisodeInfo
            {
                count = 1,
                pages = 1,
                next = "url_next",
                prev = "url_prev"
            },
            results =
            [
                new EpisodeDetail
                {
                    id = 1,
                    name = "Test"
                }
            ]
        };
        Client.GetEpisodes(Arg.Any<int?>()).Returns(mokList);

        var mokDetail = new EpisodeDetail
        {
            id = 10,
            name = "detalle",
            episode = "E00S00"
        };
        Client.GetEpisodeDetail(Arg.Any<int>()).Returns(mokDetail);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(1)]
    public async Task ObtenerEpisodios_DeberiaGenerar_CuandoPagina(int? page)
    {
        IConsultasEpisode servicio = new ServicioEpisodes(Client);
        var resultado = await servicio.GetLista(page);

        var compare = new EpisodesDto
        {
            info = new EpisodeInfoDto
            {
                pages = 1,
                count = 1
            },
            results =
            [
                new EpisodeDetailDto
                {
                    id = 1,
                    name = "Test"
                }
            ]
        };

        Assert.Equivalent(compare, resultado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task ObtenerDetalleEpisodio_DeberiaGenerar_CuandoId(int id)
    {
        IConsultasEpisode servicio = new ServicioEpisodes(Client);
        var resultado = await servicio.GetDetalle(id);

        var compare = new EpisodeFullDetailDto
        {
            id = 10,
            name = "detalle",
            episode = "E00S00"
        };

        Assert.Equivalent(compare, resultado);
    }
}