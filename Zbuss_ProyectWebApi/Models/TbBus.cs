using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbBus
    {
        public TbBus()
        {
            TbDetalleViaje = new HashSet<TbDetalleViaje>();
        }

        public int Idbus { get; set; }
        public string Placa { get; set; }
        public int Capacidad { get; set; }
        public int Pisos { get; set; }

        public virtual ICollection<TbDetalleViaje> TbDetalleViaje { get; set; }
    }
}
