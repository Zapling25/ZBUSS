using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbDetalleViaje
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

        public virtual TbBus IdbusNavigation { get; set; }
    }
}
