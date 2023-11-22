using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Vuelo
{
    public int VueloId { get; set; }

    public int? AvionId { get; set; }

    public int? AerolineaId { get; set; }

    public int? DestinoId { get; set; }

    public virtual Aerolinea? Aerolinea { get; set; }

    public virtual Avione? Avion { get; set; }

    public virtual Destino? Destino { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
