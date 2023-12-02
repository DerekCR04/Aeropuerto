using System;
using System.Collections.Generic;

namespace Aeropuerto.ViewModel
{
    public class InfoVueloViewModel
    {
        public int VueloId { get; set; }
        public string ModeloAvion { get; set; }
        public string NombreAerolinea { get; set; }
        public string Pais { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
    }
}
