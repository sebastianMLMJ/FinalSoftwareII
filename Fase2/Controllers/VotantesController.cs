using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Fase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotantesController : ControllerBase
    {
        private readonly VotacionesContext _context = new VotacionesContext();

        // POST: api/Votantes
        [HttpPost]
        public async Task<ActionResult<Votante>> PostVotante(Votante votante)
        {
          if (_context.Votantes == null)
          {
              return Problem("Entity set 'VotacionesContext.Votantes'  is null.");
          }
            _context.Votantes.Add(votante);
            await _context.SaveChangesAsync();

            return Ok("Se registro como votante");
        }
    }
}
