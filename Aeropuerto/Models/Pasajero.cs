using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Pasajero
{
    public int PasajeroId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public int? Edad { get; set; }

    public int? DestinoId { get; set; }

    public virtual Destino? Destino { get; set; }
}
