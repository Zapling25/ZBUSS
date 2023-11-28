using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbDetalleVenta
    {
        public int Idventa { get; set; }
        public int Iduser { get; set; }
        public int Idasiento { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaVenta { get; set; }
        public decimal SubTotal { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string MetodoPago { get; set; }
        public decimal Igv { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public int Idcuenta { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaViaje { get; set; }
        public bool Estado { get; set; }

        public virtual TbAsientosBus IdasientoNavigation { get; set; }
        public virtual TbPasajero IduserNavigation { get; set; }
        public virtual TbUsuarios IdusuarioNavigation { get; set; }
    }
}
