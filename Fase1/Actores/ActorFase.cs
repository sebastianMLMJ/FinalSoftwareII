using Akka.Actor;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Fase1.Actores
{
    public class ActorFase : ReceiveActor
    {
        public ActorFase() {

            ReceiveAsync <bool>( async respuesta =>
            {
                var _context = new VotacionesContext();
                var fase = await _context.FaseCrearCandidatos.FirstOrDefaultAsync();

                if (fase == null || fase.Activa == 0)
                {
                    Sender.Tell(false);
                }
                else
                {
                    Sender.Tell(true);
                }
            });
        
        
        
        
        }

    }
}
