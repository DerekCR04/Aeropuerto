using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Piloto
{
    public int PilotoId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public int? Edad { get; set; }

    public int? AerolineaId { get; set; }

    public virtual Aerolinea? Aerolinea { get; set; }

    public virtual ICollection<Avione> Avions { get; set; } = new List<Avione>();
}
