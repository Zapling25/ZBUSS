using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbUsuarios
    {
        public int IdUsuario { get; set; }
        public string TipoDoc { get; set; }
        public string NroDocumento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Celular { get; set; }
        public int IdRol { get; set; }

        public virtual TbRoles IdRolNavigation { get; set; }
    }
}
