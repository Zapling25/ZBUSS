using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbBus
    {
        public TbBus()
        {
            TbAsientosBus = new HashSet<TbAsientosBus>();
            TbDetalleViaje = new HashSet<TbDetalleViaje>();
        }

        public int Idbus { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(6)]
        public string Placa { get; set; }
        public int Capacidad { get; set; }
        public int Pisos { get; set; }  

        public virtual ICollection<TbAsientosBus> TbAsientosBus { get; set; }
        public virtual ICollection<TbDetalleViaje> TbDetalleViaje { get; set; }
    }
}
