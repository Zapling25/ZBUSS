using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbUsuarios
    {
        public TbUsuarios()
        {
            TbDetalleVenta = new HashSet<TbDetalleVenta>();
        }

        public int IdUsuario { get; set; }
        [DataType(DataType.Text)]
        public string TipoDoc { get; set; }
        [DataType(DataType.Text)]
        public string NroDocumento { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Nombres { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Apellidos { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Correo { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Contrasena { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Celular { get; set; }
        public int Rol { get; set; }
        public bool Estado { get; set; }
        public bool Reestablecer { get; set; }

        public virtual TbRoles IdRolNavigation { get; set; }

        public virtual ICollection<TbDetalleVenta> TbDetalleVenta { get; set; }
    }
}
