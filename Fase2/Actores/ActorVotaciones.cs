using Akka.Actor;
using Microsoft.EntityFrameworkCore;
using Modelos.Models;

namespace Fase2.Actores
{
    public class ActorVotaciones : ReceiveActor
    {
        public ActorVotaciones()
        {

            ReceiveAsync<bool>(async respuesta =>
            {
                var _context = new VotacionesContext();
                var fase = await _context.FaseVotaciones.FirstOrDefaultAsync();

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
