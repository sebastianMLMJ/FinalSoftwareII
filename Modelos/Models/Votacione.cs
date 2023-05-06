using System;
using System.Collections.Generic;

namespace Modelos.Models;

public partial class Votacione
{
    public int IdVoto { get; set; }

    public int? IdCandidato { get; set; }

    public virtual Candidato? IdCandidatoNavigation { get; set; }
}
