using System;
using System.Collections.Generic;

namespace Modelos.Models;

public partial class Votante
{
    public int IdVotante { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Votacione> Votaciones { get; set; } = new List<Votacione>();
}
