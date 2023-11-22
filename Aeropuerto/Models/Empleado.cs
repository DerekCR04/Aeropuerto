using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Cargo { get; set; }

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
