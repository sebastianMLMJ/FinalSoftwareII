﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;
using Fase2.Actores;

namespace Fase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotacionesController : ControllerBase
    {
        private readonly VotacionesContext _context = new VotacionesContext();

        // POST: api/Votaciones
        [HttpPost]
        public async Task<ActionResult<Votacione>> PostVotacione(Votacione votacione)
        {
          if (_context.Votaciones == null)
          {
              return Problem("Entity set 'VotacionesContext.Votaciones'  is null.");
          }

            var votacionesabiertas = await ComprobarVotacionesAbiertas();

            if (votacionesabiertas == false)
            {
                return BadRequest("Las votaciones no se encuentran abiertas");

            }
            else
            {
                var cantvotos = await _context.Votaciones.Where(p => p.IdVotante == votacione.IdVotante).CountAsync();
                if (cantvotos ==0)
                {
                    votacione.FechaHora = DateTime.Now;
                    _context.Votaciones.Add(votacione);
                    await _context.SaveChangesAsync();
                    return Ok("Su voto ha sido agregado");
                }
                else
                {
                    await Fraudes();
                    return BadRequest("Se esta intentando cometer Fraude");
                }
               
            }
        }

        [HttpGet]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> ComprobarVotacionesAbiertas()
        {
            var sistema = ActorSystem.Create("SegundoSistema");
            var actordos = sistema.ActorOf<ActorVotaciones>("actordos");
            var respuesta = await actordos.Ask<bool>(true);
            sistema.Stop(actordos);
            return respuesta;
        }

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Fraudes()
        {
           var fraude = _context.Fraudes.FirstOrDefault();

            if (fraude == null)
            {
                Fraude fraudeclass = new Fraude() { Fraudes = 1};
                _context.Fraudes.Add(fraudeclass);
                await _context.SaveChangesAsync();
            }
            else
            {
                fraude.Fraudes += 1;
                _context.Fraudes.Update(fraude);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
