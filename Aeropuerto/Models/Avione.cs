using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Avione
{
    public int AvionId { get; set; }

    public string? Modelo { get; set; }

    public int? Capacidad { get; set; }

    public int? AerolineaId { get; set; }

    public virtual Aerolinea? Aerolinea { get; set; }

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();

    public virtual ICollection<Piloto> Pilotos { get; set; } = new List<Piloto>();
}
