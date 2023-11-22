using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Destino
{
    public int DestinoId { get; set; }

    public string? NombreCiudad { get; set; }

    public string? Pais { get; set; }

    public virtual ICollection<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
