using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Aerolinea
{
    public int AerolineaId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Avione> Aviones { get; set; } = new List<Avione>();

    public virtual ICollection<Piloto> Pilotos { get; set; } = new List<Piloto>();

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
