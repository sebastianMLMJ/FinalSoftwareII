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
    public class FaseVotacionesController : ControllerBase
    {
        private readonly VotacionesContext _context = new VotacionesContext();

        // POST: api/FaseVotaciones
        [HttpPost]
        public async Task<ActionResult> AbrirVotaciones()
        {
            if (_context.FaseVotaciones == null)
            {
                return Problem("Entity set 'VotacionesContext.FaseVotaciones'  is null.");
            }
            var countFases = await _context.FaseVotaciones.CountAsync();
            if (countFases == 0)
            {
               
                FaseVotacione fase = new FaseVotacione() { Activa = 1 };
                _context.FaseVotaciones.Add(fase);
                await _context.SaveChangesAsync();
            }
            else
            {
                var fase = _context.FaseVotaciones.FirstOrDefault();
                fase.Activa = 1;
                _context.Update(fase);
                await _context.SaveChangesAsync();
            }

            return Ok("La fase de votaciones esta abierta");
        }

        // DELETE: api/FaseCrearCandidatoes
        [HttpDelete]
        public async Task<ActionResult> CerrarFaseVotaciones()
        {
            var fase = await _context.FaseVotaciones.FirstOrDefaultAsync();

            if (fase == null || fase.Activa == 0)
            {
                return Ok("La fase de crear votos ya esta cerrada");
            }
            else
            {
                fase.Activa = 0;
                _context.Update(fase);
                await _context.SaveChangesAsync();
                return Ok("La fase de votaciones se ha cerrado");
            }
        }
    }
}
