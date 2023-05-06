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
    public class FaseCrearCandidatoesController : ControllerBase
    {
        private readonly VotacionesContext _context = new VotacionesContext();
        
        // POST: api/FaseCrearCandidatoes
        [HttpPost]
        public async Task<ActionResult> AbrirFaseCrearCandidato()
        {
          if (_context.FaseCrearCandidatos == null)
          {
              return Problem("Entity set 'VotacionesContext.FaseCrearCandidatos'  is null.");
          }
            var countFases = await _context.FaseCrearCandidatos.CountAsync();
            if (countFases == 0)
            {
                FaseCrearCandidato fase = new FaseCrearCandidato() { Activa = 1 };
                _context.FaseCrearCandidatos.Add(fase);
                await _context.SaveChangesAsync();
            }
            else
            {
                var fase = _context.FaseCrearCandidatos.FirstOrDefault();
                fase.Activa = 1;
                _context.Update(fase);
                await _context.SaveChangesAsync();
            }

            return Ok("La fase de creacion de cantidatos esta abierta");

        }

        // POST: api/FaseCrearCandidatoes
        [HttpDelete]
        public async Task<ActionResult> CerrarFaseCrearCandidato()
        {
            var fase = await _context.FaseCrearCandidatos.FirstOrDefaultAsync();

            if (fase == null || fase.Activa == 0)
            {
                return Ok("La fase de crear candidatos ya esta cerrada");
            }
            else
            {
                fase.Activa = 0;
                _context.Update(fase);
                await _context.SaveChangesAsync();  
                return Ok("La fase de creacion de cantidatos se ha cerrado");
            }
        }
    }
}
