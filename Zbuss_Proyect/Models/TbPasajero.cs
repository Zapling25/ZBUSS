using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbPasajero
    {
        public TbPasajero()
        {
            TbDetalleVenta = new HashSet<TbDetalleVenta>();
        }

        public int Iduser { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(22)]
        public string TpoDocumento { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(8)]
        public string NroDocumento { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Nombre { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ApePaterno { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ApeMaterno { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Correo { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(9)]
        public string Celular { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string Sexo { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNac { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<TbDetalleVenta> TbDetalleVenta { get; set; }
    }
}
