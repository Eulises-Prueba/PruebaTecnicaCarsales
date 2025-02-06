using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebFront.Api.Controllers
{
    /// <summary>
    /// Controlador para manejo de errores globales para usuario final
    /// </summary>
    /// <param name="logger"></param>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController(ILogger<ErrorController> logger) : ControllerBase
    {
        /// <summary>
        /// Captura y almacena error para usuario final
        /// </summary>
        /// <returns>500</returns>
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var routeData = exceptionHandler.RouteValues;

            var error = exceptionHandler.Error.GetBaseException();
            logger.LogError(error, error.Message, routeData);
            return Problem();
        }
    }
}