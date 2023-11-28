using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSZbuss_Proyect.ViewModels
{
    public class vmDetalleviaje
    {
        public int Idviaje { get; set; }
        public int Idbus { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public string PuntoPartida { get; set; }
        public string PuntoLlegada { get; set; }
        public int NroAsientos { get; set; }
        public int DuracionViaje { get; set; }

    }
}
