using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbAsientosBus
    {
        public TbAsientosBus()
        {
            TbDetalleVenta = new HashSet<TbDetalleVenta>();
        }

        public int Idasiento { get; set; }
        public int Idbus { get; set; }
        public int CodAsiento { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(4)]
        public string Inclinacion { get; set; }
        public decimal Precio { get; set; }
        public int PisoBus { get; set; }
        public bool Estado { get; set; }

        public virtual TbBus IdbusNavigation { get; set; }
        public virtual ICollection<TbDetalleVenta> TbDetalleVenta { get; set; }
    }
}
