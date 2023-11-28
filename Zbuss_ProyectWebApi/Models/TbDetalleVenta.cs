using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbDetalleVenta
    {
        public int Idventa { get; set; }
        public int? Iduser { get; set; }
        public int Idasiento { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal? SubTotal { get; set; }
        public string MetodoPago { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
    }
}
