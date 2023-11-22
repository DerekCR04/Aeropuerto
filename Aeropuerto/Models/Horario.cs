using System;
using System.Collections.Generic;

namespace Aeropuerto.Models;

public partial class Horario
{
    public int HorarioId { get; set; }

    public TimeSpan? HoraSalida { get; set; }

    public TimeSpan? HoraLlegada { get; set; }

    public int? VueloId { get; set; }

    public virtual Vuelo? Vuelo { get; set; }
}
