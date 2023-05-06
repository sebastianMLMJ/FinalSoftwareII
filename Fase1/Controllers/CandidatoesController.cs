using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Fase1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoesController : ControllerBase
    {
        private readonly VotacionesContext _context = new VotacionesContext();

        // POST: api/Candidatoes
        [HttpPost]
        public async Task<ActionResult> PostCandidato(Candidato candidato)
        {
            var faseabierta = await ComprobarFaseAbierta();

            if (faseabierta==true)
            {
                _context.Candidatos.Add(candidato);
                await _context.SaveChangesAsync();
                return Ok("El candidato " + candidato.Nombre + " Ha sido creado");
            }
            else {

                return BadRequest("La fase de ingreso de candidatos se encuentra cerrada");
            }
            
        }
        // GET: Fases
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task<bool> ComprobarFaseAbierta()
        {
            var fase = await _context.FaseCrearCandidatos.FirstOrDefaultAsync();
            
            if (fase == null || fase.Activa==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
