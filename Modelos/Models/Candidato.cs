using System;
using System.Collections.Generic;

namespace Modelos.Models;

public partial class Candidato
{
    public int IdCandidato { get; set; }

    public string Nombre { get; set; } = null!;

    public string Partido { get; set; } = null!;

    public virtual ICollection<Votacione> Votaciones { get; set; } = new List<Votacione>();
}
