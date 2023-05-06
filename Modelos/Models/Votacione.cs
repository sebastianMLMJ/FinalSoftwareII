using System;
using System.Collections.Generic;

namespace Modelos.Models;

public partial class Votacione
{
    public int IdVoto { get; set; }

    public int? IdCandidato { get; set; }

    public int? IdVotante { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Ip { get; set; }

    public virtual Candidato? IdCandidatoNavigation { get; set; }

    public virtual Votante? IdVotanteNavigation { get; set; }
}
