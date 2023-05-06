using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Fase1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {

        private readonly VotacionesContext _context = new VotacionesContext();

        [HttpGet]
        public async Task<IActionResult> Estadisticas()
        {
            var fraudes = _context.Fraudes.FirstOrDefault();
            int? cantfraudes = 0;

            var cantvotos = await _context.Votaciones.CountAsync();
            if (fraudes != null)
            {
                cantfraudes = fraudes.Fraudes;
            }
            return Ok("la cantidad de votos es " + cantvotos.ToString() + " La cantidad de fraudes es :" + cantfraudes);
        }
    }
}
