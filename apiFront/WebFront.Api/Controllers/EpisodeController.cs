using Microsoft.AspNetCore.Mvc;
using WebFront.Core.Interface;

namespace WebFront.Api.Controllers
{
    /// <summary>
    /// Controlador de Episodios
    /// </summary>
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController(IConsultasEpisode core) : ControllerBase
    {
        internal const int CACHE_LISTA = 15;
        internal const int CACHE_DETALLE = 5;

        /// <summary>
        /// Obtiene listado de episodios
        /// </summary>
        [HttpGet]
        [ResponseCache(Duration = CACHE_LISTA, VaryByQueryKeys = ["page"])]
        public async Task<IActionResult> GetListado(int? page)
        {
            if (page <= 0)
                return BadRequest("Parámetro 'page' es invalido.");

            var lstEpisodes = await core.GetLista(page);
            return Ok(lstEpisodes);
        }

        /// <summary>
        /// Obtiene detalle del episodio
        /// </summary>
        [HttpGet("{id:int}")]
        [ResponseCache(Duration = CACHE_DETALLE, VaryByQueryKeys = ["id"])]
        public async Task<IActionResult> GetDetalle(int id)
        {
            if (id <= 0)
                return BadRequest("Parámetro 'id' es invalido.");

            var episode = await core.GetDetalle(id);
            return Ok(episode);
        }
    }
}
